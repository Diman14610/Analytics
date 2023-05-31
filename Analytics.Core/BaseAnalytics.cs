using Analytics.Core.Interfaces;
using Analytics.Methods;
using System.Linq.Expressions;

namespace Analytics.Core
{
    public class BaseAnalytics
    {
        protected readonly IHandlersManager _hanlderManager;

        // method name, analytics type
        protected readonly List<MethodStructure> _selectedMethods;

        public BaseAnalytics()
        {
            _hanlderManager = new HandlersManager();

            _selectedMethods = new List<MethodStructure>();
        }

        public BaseAnalytics CheckFor(params Expression<Func<MajorMethods, object>>[] selectedFn)
        {
            FillInternalListSelectedMethods(selectedFn, typeof(CheckResult));
            return this;
        }

        public BaseAnalytics EqualsTo(params Expression<Func<MajorMethods, object>>[] selectedFn)
        {
            FillInternalListSelectedMethods(selectedFn, typeof(EqualsResult));
            return this;
        }

        public AnalyticsResult Analysis(string text)
        {
            var analyticsResult = new AnalyticsResult();

            foreach (MethodStructure selectedMethod in _selectedMethods)
            {
                _hanlderManager.Handle(analyticsResult, selectedMethod.Type, selectedMethod.Methods, text);
            }

            return analyticsResult;
        }

        protected void FillInternalListSelectedMethods(Expression<Func<MajorMethods, object>>[] selectedFn, Type type)
        {
            _selectedMethods.Add(new MethodStructure(type, GetMethodsList(selectedFn)));
        }

        protected IEnumerable<string> GetMethodsList(Expression<Func<MajorMethods, object>>[] selectedFn)
        {
            var list = new List<string>();

            foreach (var majorMethod in selectedFn)
            {
                try
                {
                    list.Add(GetMethodName(majorMethod));
                }
                catch (ArgumentNullException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return list;
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