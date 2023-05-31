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
            try
            {
                return _hanlderManager.Handle<CheckResult>(methods, text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected EqualsResult EqualsTo(IEnumerable<string> methods, string text)
        {
            try
            {
                return _hanlderManager.Handle<EqualsResult>(methods, text);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
