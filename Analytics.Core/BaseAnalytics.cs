using Analytics.Handlers;
using Analytics.Shared;

namespace Analytics.Core
{
    public class BaseAnalytics
    {
        protected readonly IHandlersManager _hanlderManager;

        public BaseAnalytics(IHandlersManager handler)
        {
            _hanlderManager = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        protected CheckResult CheckFor(IEnumerable<string> methods, string text)
        {
            return _hanlderManager.Handle<CheckResult>(methods, text);
        }

        protected CheckResult CheckFor(string text, TextFactory textFactory)
        {
            return _hanlderManager.Handle<CheckResult>(text, textFactory.SelectedMethods);
        }

        protected EqualsResult EqualsTo(IEnumerable<string> methods, string text)
        {
            return _hanlderManager.Handle<EqualsResult>(methods, text);
        }
    }
}
