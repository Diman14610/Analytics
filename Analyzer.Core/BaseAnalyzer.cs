using Analyzer.Methods;
using System.Linq.Expressions;
using System.Reflection;

namespace Analyzer.Core
{
    public class BaseAnalyzer
    {
        private readonly IMethodsList _methodsList;

        private readonly List<string> _selectedMethods;

        private readonly string _text;

        public BaseAnalyzer(string text)
        {
            _methodsList = new MethodsManager();

            _selectedMethods = new List<string>();
            _text = text;
        }

        public BaseAnalyzer CheckFor(params Expression<Func<MajorMethods, object>>[] selectedMM)
        {
            foreach (var majorMethod in selectedMM)
            {
                try
                {
                    _selectedMethods.Add(GetMethodName(majorMethod));
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

            return this;
        }

        public IEnumerable<AnalysisResult> Analiz()
        {
            var results = new List<AnalysisResult>();

            foreach (var method in _selectedMethods)
            {
                Func<string, bool>? getedMethod = _methodsList.TryGetMethod(method);

                var analysisResult = new AnalysisResult
                {
                    MethodName = method,
                    IsMethodFound = getedMethod == null
                };

                try
                {
                    analysisResult.IsEqual = getedMethod != null && getedMethod(_text);
                }
                catch (Exception ex)
                {
                    analysisResult.IsEqual = false;
                    analysisResult.IsError = true;
                    analysisResult.Exception = ex;
                }

                results.Add(analysisResult);
            }

            return results;
        }

        private string GetMethodName(LambdaExpression expression)
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