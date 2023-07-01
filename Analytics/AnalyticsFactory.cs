using Analytics.Configuration;
using Analytics.Core;
using Analytics.Handlers;
using Analytics.Methods.SharedMethods;
using Analytics.Shared.Analytics;

namespace Analytics
{
    public sealed class AnalyticsFactory : BaseAnalytics
    {
        private readonly MajorMethods _majorMethods;
        private readonly MethodsWithArguments _methodsWithArguments;

        private readonly ICollection<(Type, MethodsFactory methodsFactory)> _selectedMethods;

        public AnalyticsFactory(IHandlersManager handler) : base(handler)
        {
            _majorMethods = new MajorMethods();
            _methodsWithArguments = new MethodsWithArguments();

            _selectedMethods = new List<(Type, MethodsFactory methodsFactory)>();
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
            var analyticsResult = new AnalyticsResult();

            HandleAnalytics(text, analyticsResult);

            return analyticsResult;
        }

        private void HandleAnalytics(string text, AnalyticsResult analyticsResult)
        {
            foreach ((Type type, MethodsFactory textFactory) in _selectedMethods)
            {
                analyticsResult.Text = text;

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
            var _ = new MethodsFactory(_majorMethods, _methodsWithArguments, (IConfigurationProvider)Configuration);
            methodsFactory(_);

            _selectedMethods.Add((type, _));
        }
    }
}
