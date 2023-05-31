using Analytics.Core.Interfaces;
using Analytics.Methods;

namespace Analytics.Core
{
    public class HandlersManager : IHandlersManager
    {
        private readonly IMethodsList _methodsList;

        public HandlersManager()
        {
            _methodsList = new MethodsManager();
        }

        public void Handle(AnalyticsResult analyticsResult, Type type, IEnumerable<string> methods, string text)
        {
            analyticsResult.Text = text;
            //var a = _handlers[type].Handle(methods, text) as CheckResult;

            if (type == typeof(CheckResult))
            {
                var checkResult = new CheckHandler(_methodsList).Handle(methods, text);
                analyticsResult.CheckResult.Add(checkResult);
            }
            else if (type == typeof(EqualsResult))
            {
                var equalsResult = new EqualsHandler(_methodsList).Handle(methods, text);
                analyticsResult.EqualsResult.Add(equalsResult);
            }
        }
    }
}