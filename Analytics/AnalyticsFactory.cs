using Analytics.Core;
using Analytics.Handlers;
using Analytics.Methods.SharedMethods;
using Analytics.Shared;

namespace Analytics
{
    public class AnalyticsFactory : BaseAnalytics
    {
        private readonly MajorMethods _majorMethods;
        private readonly MethodsWithArguments _methodsWithArguments;

        protected readonly ICollection<(Type, MethodsFactory methodsFactory)> _selectedMethods;

        public AnalyticsFactory(IHandlersManager handler) : base(handler)
        {
            _majorMethods = new MajorMethods();
            _methodsWithArguments = new MethodsWithArguments();

            _selectedMethods = new List<(Type, MethodsFactory methodsFactory)>();
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

        public void AddToMethodsList(Action<MethodsFactory> methodsFactory, Type type)
        {
            var _ = new MethodsFactory(_majorMethods, _methodsWithArguments);
            methodsFactory(_);

            _selectedMethods.Add((type, _));
        }

        public AnalyticsResult Analysis(string text)
        {
            var analyticsResult = new AnalyticsResult();

            HandleMajorAnalytics(text, analyticsResult);

            return analyticsResult;
        }

        private void HandleMajorAnalytics(string text, AnalyticsResult analyticsResult)
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
    }
}
