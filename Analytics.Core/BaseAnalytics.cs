using Analytics.Methods;
using System.Linq.Expressions;
using System.Reflection;

namespace Analytics.Core
{
    public interface IHandler
    {
        public void Handle(AnalyticsResult analyticsResult, string method, string text);
    }

    public interface IHanlderManager
    {
        public AnalyticsResult Calc(Type type, IEnumerable<string> methods, string text);
    }

    public abstract class BaseHandler<T> where T : class
    {
        protected readonly IMethodsList _methodsList;

        public Type Type => typeof(T);

        protected BaseHandler()
        {
            _methodsList = new MethodsManager();
        }

        public abstract T Handle(IEnumerable<string> methods, string text);
    }

    public class EqualsHandler : BaseHandler<EqualsResult>
    {
        public override EqualsResult Handle(IEnumerable<string> methods, string text)
        {
            throw new NotImplementedException();
        }
    }

    public class CheckHandler : BaseHandler<IEnumerable<CheckResult>>
    {
        public override IEnumerable<CheckResult> Handle(IEnumerable<string> methods, string text)
        {
            throw new NotImplementedException();
        }
    }

    //public class EqualsHandler : BaseHandler
    //{
    //    public override AnalyticsType Type => AnalyticsType.Equals;

    //    public override void Handle(AnalyticsResult analyticsResult, string method, string text)
    //    {
    //        Func<string, bool>? getedMethod = _methodsList.TryGetMethod(method);

    //        analyticsResult.IsMethodFound = getedMethod != null;

    //        try
    //        {
    //            analyticsResult.IsEqual = getedMethod != null && getedMethod(text);
    //        }
    //        catch (Exception ex)
    //        {
    //            analyticsResult.IsEqual = false;
    //            analyticsResult.IsError = true;
    //            analyticsResult.Exception = ex;
    //        }
    //    }
    //}


    public class HandlerManager : IHanlderManager
    {
        public AnalyticsResult Calc(Type type, IEnumerable<string> methods, string text)
        {
            var analyticsResult = new AnalyticsResult();

            if (type == typeof(EqualsResult))
            {
                analyticsResult.EqualsResult = new EqualsHandler().Handle(methods, text);
            }
            else if (type == typeof(CheckResult))
            {
                analyticsResult.CheckResults = new CheckHandler().Handle(methods, text);
            }

            return analyticsResult;
        }
    }

    public class BaseAnalytics
    {
        private readonly IHanlderManager _hanlderManager;

        // method name, analytics type
        private readonly List<(string, Type)> _selectedMethods;

        public BaseAnalytics()
        {
            _hanlderManager = new HandlerManager();

            _selectedMethods = new List<(string, Type)>();
        }

        public BaseAnalytics CheckFor(params Expression<Func<MajorMethods, object>>[] selectedFn)
        {
            Fill(selectedFn, typeof(CheckResult));
            return this;
        }

        public BaseAnalytics EqualsTo(params Expression<Func<MajorMethods, object>>[] selectedFn)
        {
            Fill(selectedFn, typeof(EqualsResult));
            return this;
        }

        public IEnumerable<AnalyticsResult> Analysis(string text)
        {
            var results = new List<AnalyticsResult>();

            var t = _selectedMethods.GroupBy(m => m.Item2).ToList();

            foreach (var t2 in t)
            {
                var a2 = t2.Select(t2 => t2.Item1).ToList();

                results.Add(_hanlderManager.Calc(t2.Key, a2, text));
            }

            return results;
        }

        protected void Fill(Expression<Func<MajorMethods, object>>[] selectedFn, Type type)
        {
            _selectedMethods.AddRange(GetMethodsList(selectedFn).Select(s => (s, type)));
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
                var methodInfo = (MethodInfo)methodCallObject.Value!;

                return methodInfo.Name;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get the name of the selected method.", ex);
            }
        }
    }
}