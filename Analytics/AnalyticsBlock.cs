using Analytics.Configuration;
using Analytics.Core;
using Analytics.Methods;
using Analytics.Root;
using Analytics.Shared.Analytics;
using System.Collections.Concurrent;

namespace Analytics
{
    public sealed class AnalyticsBlock : BaseAnalytics
    {
        private readonly List<(Type, MethodsConstructorProvider)> _selectedMethods;

        public AnalyticsBlock() : base()
        {
            _selectedMethods = new List<(Type, MethodsConstructorProvider)>();
        }

        public AnalyticsBlock Configure(Action<AnalyticsConfiguration> configuration)
        {
            configuration(Configuration);

            object? settings = ((AnalyticsConfigurationProvider)Configuration).GetSettings();
            if (settings != null)
            {
                _selectedMethods.AddRange((IEnumerable<(Type, MethodsConstructorProvider)>)settings);
            }

            return this;
        }

        public AnalyticsConfiguration AsAnalyticsConfiguration()
        {
            var configurationProvider = (AnalyticsConfigurationProvider)Configuration;
            configurationProvider.SaveSettings(_selectedMethods);
            return Configuration;
        }

        public AnalyticsBlock CheckFor(Action<MethodsConstructor> methodFactory)
        {
            AddToMethodsList<CheckResult>(methodFactory);
            return this;
        }

        public AnalyticsBlock EqualsTo(Action<MethodsConstructor> methodFactory)
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

        private void AddToMethodsList<T>(Action<MethodsConstructor> methodsConstructor)
        {
            var factoryProvider = new MethodsConstructorProvider(
                DefaultDependencies.GetMajorMethods(),
                DefaultDependencies.GetMethodsWithArguments(),
                (AnalyticsConfigurationProvider)Configuration
                );

            methodsConstructor(factoryProvider);

            _selectedMethods.Add((typeof(T), factoryProvider));
        }
    }
}
