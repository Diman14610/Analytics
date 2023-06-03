using Analytics.Core;
using Analytics.Handlers;
using Analytics.Methods.SharedMethods;
using Analytics.Shared;
using System.Linq.Expressions;

namespace Analytics
{
    public class AnalyticsFactory : BaseAnalytics
    {
        protected readonly ICollection<(Type, IEnumerable<string>)> _selectedMethods;

        public AnalyticsFactory(IHandlersManager handler) : base(handler)
        {
            _selectedMethods = new List<(Type, IEnumerable<string>)>();
        }

        public AnalyticsFactory EqualsTo(params Expression<Func<MajorMethods, object>>[] selectedFn)
        {
            FillInternalListSelectedMethods(selectedFn, typeof(EqualsResult));
            return this;
        }

        public AnalyticsFactory CheckFor(params Expression<Func<MajorMethods, object>>[] selectedFn)
        {
            FillInternalListSelectedMethods(selectedFn, typeof(CheckResult));
            return this;
        }

        public AnalyticsResult Analysis(string text)
        {
            var analyticsResult = new AnalyticsResult();

            foreach ((Type type, IEnumerable<string> methods) in _selectedMethods)
            {
                analyticsResult.Text = text;

                if (type == typeof(CheckResult))
                {
                    CheckResult checkResult = CheckFor(methods, text);

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
                    EqualsResult equalsResult = EqualsTo(methods, text);

                    if (analyticsResult.EqualsResult == null)
                    {
                        analyticsResult.EqualsResult = new List<EqualsResult>() { equalsResult };
                    }
                    else
                    {
                        analyticsResult.EqualsResult.Add(equalsResult);
                    }
                }
            }

            return analyticsResult;
        }

        protected void FillInternalListSelectedMethods(Expression<Func<MajorMethods, object>>[] selectedFn, Type type)
        {
            _selectedMethods.Add((type, GetMethodsList(selectedFn)));
        }

        protected IEnumerable<string> GetMethodsList(Expression<Func<MajorMethods, object>>[] selectedFn)
        {
            var methodsList = new List<string>();

            try
            {
                foreach (var majorMethod in selectedFn)
                {
                    methodsList.Add(GetMethodName(majorMethod));
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return methodsList;
        }

        protected string GetMethodName(LambdaExpression expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            try
            {
                var unaryExpression = (UnaryExpression)expression.Body;
                var methodCallExpression = (MethodCallExpression)unaryExpression.Operand;
                var methodCallObject = (ConstantExpression)methodCallExpression.Object!;
                var methodInfo = (System.Reflection.MethodInfo)methodCallObject.Value!;

                return methodInfo.Name;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get the name of the selected method.", ex);
            }
        }
    }
}
