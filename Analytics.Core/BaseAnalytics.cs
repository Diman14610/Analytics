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

        protected CheckResult CheckFor(string text, MethodsFactory methodsFactory)
        {
            var result = new CheckResult();

            if (methodsFactory.SelectedMethods.MajorFactoryMethod.Count > 0)
            {
                _hanlderManager.Handle(text, methodsFactory.SelectedMethods.MajorFactoryMethod, result);
            }
            if (methodsFactory.SelectedMethods.TextFactoryMethod.Count > 0)
            {
                _hanlderManager.Handle(text, methodsFactory.SelectedMethods.TextFactoryMethod, result);
            }

            return result;
        }

        protected EqualsResult EqualsTo(string text, MethodsFactory methodsFactory)
        {
            var result = new EqualsResult();

            if (methodsFactory.SelectedMethods.TextFactoryMethod.Count > 0)
            {
                _hanlderManager.Handle(text, methodsFactory.SelectedMethods.TextFactoryMethod, result);
            }
            if (methodsFactory.SelectedMethods.MajorFactoryMethod.Count > 0)
            {
                _hanlderManager.Handle(text, methodsFactory.SelectedMethods.MajorFactoryMethod, result);
            }

            try
            {
                result.IsEqual = result.ExtendedMethodInfos!.All(m => m.IsEqual);
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Exception = ex;
            }

            return result;
        }
    }
}
