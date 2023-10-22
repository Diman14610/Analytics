using Analytics.Configuration;
using Analytics.Core;
using Analytics.Methods;
using Analytics.Methods.SharedMethods;
using Analytics.Root;
using Analytics.Shared.Analytics;
using System.Collections.Concurrent;

namespace Analytics
{
    public sealed class AnalyticsFactory : BaseAnalytics
    {
        private readonly List<(Type, MethodsFactoryProvider)> _selectedMethods;

        public AnalyticsFactory() : base()
        {
            _selectedMethods = new List<(Type, MethodsFactoryProvider)>();
        }

        public AnalyticsFactory Configure(Action<AnalyticsConfiguration> configuration)
        {
            configuration(Configuration);

            object? settings = ((AnalyticsConfigurationProvider)Configuration).GetSettings();
            if (settings != null)
            {
                foreach (var item in (IEnumerable<(Type, MethodsFactoryProvider)>)settings)
                {
                    _selectedMethods.Add(item);
                }
            }

            return this;
        }

        public AnalyticsConfiguration AsAnalyticsConfiguration()
        {
            var configurationProvider = (AnalyticsConfigurationProvider)Configuration;
            configurationProvider.SaveSettings(_selectedMethods);
            return Configuration;
        }

        public AnalyticsFactory CheckFor(Action<MethodsFactory> methodFactory)
        {
            AddToMethodsList<CheckResult>(methodFactory);
            return this;
        }

        public AnalyticsFactory EqualsTo(Action<MethodsFactory> methodFactory)
        {
            AddToMethodsList<EqualsResult>(methodFactory);
            return this;
        }

        public AnalyticsResult Analysis(string word)
        {
            var analyticsResult = new AnalyticsResult(word);

            HandleAnalytics(word, analyticsResult);

            return analyticsResult;
        }

        public IEnumerable<AnalyticsResult> Analysis(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                yield return Analysis(word);
            }
        }

        /// <summary>
        /// Performs words analysis in parallel without preserving the order.
        /// </summary>
        public IEnumerable<AnalyticsResult> ParallelAnalysis(IEnumerable<string> words, ParallelOptions? parallelOptions = default, CancellationToken cancellationToken = default)
        {
            var analyticsResults = new ConcurrentBag<AnalyticsResult>();

            parallelOptions ??= new ParallelOptions() { CancellationToken = cancellationToken };

            Parallel.ForEach(words, parallelOptions, (word, state) =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    state.Break();
                }
                if (state.ShouldExitCurrentIteration)
                {
                    return;
                }

                analyticsResults.Add(Analysis(word));
            });

            return analyticsResults;
        }

        private void HandleAnalytics(string text, AnalyticsResult analyticsResult)
        {
            foreach (var (type, textFactory) in _selectedMethods)
            {
                if (type == typeof(CheckResult))
                {
                    analyticsResult.CheckResult.Add(CheckFor(text, textFactory));
                }
                else if (type == typeof(EqualsResult))
                {
                    analyticsResult.EqualsResult.Add(EqualsTo(text, textFactory));
                }
            }
        }

        private void AddToMethodsList<T>(Action<MethodsFactory> methodsFactory)
        {
            var factoryProvider = new MethodsFactoryProvider(
                DefaultDependencies.GetMajorMethods(),
                DefaultDependencies.GetMethodsWithArguments(),
                (AnalyticsConfigurationProvider)Configuration
                );

            methodsFactory(factoryProvider);

            _selectedMethods.Add((typeof(T), factoryProvider));
        }
    }
}
