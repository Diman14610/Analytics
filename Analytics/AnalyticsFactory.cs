using Analytics.Core;
using Analytics.Handlers;
using Analytics.Methods.SharedMethods;
using Analytics.Shared;
using System.Linq.Expressions;

namespace Analytics
{
    public class AnalyticsFactory : BaseAnalytics
    {
        private readonly MajorMethods _majorMethods;
        private readonly MethodsWithArguments _methodsWithArguments;

        protected readonly ICollection<(Type, MajorFactory majorFactory)> _selectedMajorMethods;
        protected readonly ICollection<(Type, TextFactory textFactory)> _selectedMethodsWithArguments;

        public AnalyticsFactory(IHandlersManager handler) : base(handler)
        {
            _majorMethods = new MajorMethods();
            _methodsWithArguments = new MethodsWithArguments();

            _selectedMajorMethods = new List<(Type, MajorFactory)>();
            _selectedMethodsWithArguments = new List<(Type, TextFactory)>();

        }

        public AnalyticsFactory CheckFor(Action<MajorFactory> majorFactory)
        {
            AddToMajorList(majorFactory, typeof(CheckResult));
          
            return this;
        }

        public AnalyticsFactory CheckFor(Action<TextFactory> textFactory)
        {
            AddToMethodWithArgumentsList(textFactory, typeof(CheckResult));
            return this;
        }

        public AnalyticsFactory EqualsTo(Action<MajorFactory> majorFactory)
        {
            AddToMajorList(majorFactory, typeof(EqualsResult));
            return this;
        }

        public AnalyticsFactory EqualsTo(Action<TextFactory> textFactory)
        {
            AddToMethodWithArgumentsList(textFactory, typeof(EqualsResult));
            return this;
        }

        public void AddToMajorList(Action<MajorFactory> majorFactory, Type type)
        {
            var _ = new MajorFactory(_majorMethods);
            majorFactory(_);

            _selectedMajorMethods.Add((type, _));
        }

        public void AddToMethodWithArgumentsList(Action<TextFactory> textFactory, Type type)
        {
            var _ = new TextFactory(_methodsWithArguments);
            textFactory(_);

            _selectedMethodsWithArguments.Add((type, _));
        }

        public AnalyticsResult Analysis(string text)
        {
            var analyticsResult = new AnalyticsResult();

            HandleMajorAnalytics(text, analyticsResult);

            HandleTextAnalytics(text, analyticsResult);

            return analyticsResult;
        }

        private void HandleMajorAnalytics(string text, AnalyticsResult analyticsResult)
        {
            foreach ((Type type, MajorFactory textFactory) in _selectedMajorMethods)
            {
                analyticsResult.Text = text;

                if (type == typeof(CheckResult))
                {
                    CheckResult checkResult = CheckFor(text, textFactory);

                    if (analyticsResult.CheckResult == null)
                    {
                        analyticsResult.CheckResult = new List<CheckResult>() { checkResult };
                    }
                    else
                    {
                        analyticsResult.CheckResult.Add(checkResult);
                    }
                }
                else if (type == typeof(EqualsResult))
                {
                    EqualsResult checkResult = EqualsTo(text, textFactory);

                    if (analyticsResult.EqualsResult == null)
                    {
                        analyticsResult.EqualsResult = new List<EqualsResult>() { checkResult };
                    }
                    else
                    {
                        analyticsResult.EqualsResult.Add(checkResult);
                    }
                }
            }
        }

        private void HandleTextAnalytics(string text, AnalyticsResult analyticsResult)
        {
            foreach ((Type type, TextFactory textFactory) in _selectedMethodsWithArguments)
            {
                analyticsResult.Text = text;

                if (type == typeof(CheckResult))
                {
                    CheckResult checkResult = CheckFor(text, textFactory);

                    if (analyticsResult.CheckResult == null)
                    {
                        analyticsResult.CheckResult = new List<CheckResult>() { checkResult };
                    }
                    else
                    {
                        analyticsResult.CheckResult.Add(checkResult);
                    }
                }
                else if (type == typeof (EqualsResult))
                {
                    EqualsResult checkResult = EqualsTo(text, textFactory);

                    if (analyticsResult.EqualsResult == null)
                    {
                        analyticsResult.EqualsResult = new List<EqualsResult>() { checkResult };
                    }
                    else
                    {
                        analyticsResult.EqualsResult.Add(checkResult);
                    }
                }
            }
        }
    }
}
