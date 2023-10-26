using Analytics.Configuration;
using Analytics.Core.Abstractions;
using Analytics.Handlers;
using Analytics.Methods;
using Analytics.Methods.SharedMethods;
using Analytics.Shared.Analytics;
using Analytics.Shared.Core.Analytics;
using System.Collections.Concurrent;

namespace Analytics.Core
{
    public sealed class AnalyticsBlock : BaseAnalytics
    {
        private readonly MajorMethods _majorMethods = new();
        private readonly List<(Type, MethodsConstructorProvider)> _selectedMethods = new();

        public AnalyticsBlock()
        {
        }

        public AnalyticsBlock(IHandlersManager handlersManager) : base(handlersManager)
        {
        }

        public AnalyticsBlock Configure(Action<AnalyticsConfiguration> configuration)
        {
            configuration(Configuration);
            return this;
        }

        public AnalyticsConfiguration AsAnalyticsConfiguration()
        {
            return Configuration;
        }

        public AnalyticsBlock CopyBlocks(params AnalyticsBlock[] analyticsBlocks)
        {
            foreach (var analyticsBlock in analyticsBlocks)
            {
                _selectedMethods.AddRange(analyticsBlock._selectedMethods);
            }
            return this;
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
            var analyticsResult = new AnalyticsResult();

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
            foreach ((Type type, MethodsConstructorProvider methods) in _selectedMethods)
            {
                if (type == typeof(CheckResult))
                {
                    analyticsResult.CheckResult.Add(CheckFor(text, methods));
                }
                else if (type == typeof(EqualsResult))
                {
                    analyticsResult.EqualsResult.Add(EqualsTo(text, methods));
                }
            }
        }

        private void AddToMethodsList<T>(Action<MethodsConstructor> methodsConstructor)
        {
            var factoryProvider = new MethodsConstructorProvider(
                _majorMethods,
                new MethodsWithArguments(),
                (AnalyticsConfigurationProvider)Configuration
                );

            methodsConstructor(factoryProvider);

            _selectedMethods.Add((typeof(T), factoryProvider));
        }
    }
}
