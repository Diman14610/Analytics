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

        protected CheckResult CheckFor(string text, MajorFactory majorFactory)
        {
            return _hanlderManager.Handle<CheckResult, MajorFactoryMethodInfo>(text, majorFactory.SelectedMethods);
        }

        protected CheckResult CheckFor(string text, TextFactory textFactory)
        {
            return _hanlderManager.Handle<CheckResult, TextFactoryMethodInfo>(text, textFactory.SelectedMethods);
        }

        protected EqualsResult EqualsTo(string text, MajorFactory majorFactory)
        {
            return _hanlderManager.Handle<EqualsResult, MajorFactoryMethodInfo>(text, majorFactory.SelectedMethods);
        }

        protected EqualsResult EqualsTo(string text, TextFactory textFactory)
        {
            return _hanlderManager.Handle<EqualsResult, TextFactoryMethodInfo>(text, textFactory.SelectedMethods);
        }
    }
}
