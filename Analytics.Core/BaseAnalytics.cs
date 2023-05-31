using Analytics.Handlers;
using Analytics.Shared;

namespace Analytics.Core
{
    public class BaseAnalytics
    {
        protected readonly IHandlersManager _hanlder;

        public BaseAnalytics(IHandlersManager handler)
        {
            _hanlder = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        protected CheckResult CheckFor(IEnumerable<string> methods, string text)
        {
            try
            {
                return _hanlder.Handle<CheckResult>(methods, text);
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
                return _hanlder.Handle<EqualsResult>(methods, text);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
