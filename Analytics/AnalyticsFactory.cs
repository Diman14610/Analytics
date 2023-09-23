using Analytics.Configuration;
using Analytics.Core;
using Analytics.Methods;
using Analytics.Methods.SharedMethods;
using Analytics.Shared.Analytics;

namespace Analytics
{
    public sealed class AnalyticsFactory : BaseAnalytics
    {
        private readonly MajorMethods _majorMethods;
        private readonly MethodsWithArguments _methodsWithArguments;
        private readonly List<(Type, MethodsFactoryProvider)> _selectedMethods;

        public AnalyticsFactory() : base()
        {
            _majorMethods = new MajorMethods();
            _methodsWithArguments = new MethodsWithArguments();
            _selectedMethods = new List<(Type, MethodsFactoryProvider)>();
        }

        public AnalyticsFactory Configure(Action<AnalyticsConfiguration> configuration)
        {
            configuration(Configuration);
            return this;
        }

        public AnalyticsFactory CheckFor(Action<MethodsFactory> methodFactory)
        {
            AddToMethodsList(methodFactory, typeof(CheckResult));
            return this;
        }

        public AnalyticsFactory EqualsTo(Action<MethodsFactory> methodFactory)
        {
            AddToMethodsList(methodFactory, typeof(EqualsResult));
            return this;
        }

        public AnalyticsResult Analysis(string text)
        {
            var analyticsResult = new AnalyticsResult(text);

            HandleAnalytics(text, analyticsResult);

            return analyticsResult;
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

        private void AddToMethodsList(Action<MethodsFactory> methodsFactory, Type type)
        {
            var factoryProvider = new MethodsFactoryProvider(
                _majorMethods,
                _methodsWithArguments,
                (IConfigurationProvider)Configuration
                );

            methodsFactory(factoryProvider);

            _selectedMethods.Add((type, factoryProvider));
        }
    }
}
