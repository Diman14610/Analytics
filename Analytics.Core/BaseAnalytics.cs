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
            var checkResult = new CheckResult();

            CallToHandler(text, methodsFactory, checkResult);

            return checkResult;
        }

        protected EqualsResult EqualsTo(string text, MethodsFactory methodsFactory)
        {
            var equalsResult = new EqualsResult();

            CallToHandler(text, methodsFactory, equalsResult);

            try
            {
                equalsResult.IsEqual = equalsResult.ExtendedMethodInfos!.All(m => m.IsEqual);
            }
            catch (Exception ex)
            {
                equalsResult.IsError = true;
                equalsResult.Exception = ex;
            }

            return equalsResult;
        }

        private void CallToHandler<T>(string text, MethodsFactory methodsFactory, T value)
        {
            if (methodsFactory.SelectedMethods.MajorFactoryMethod.Count > 0)
            {
                _hanlderManager.Handle(text, methodsFactory.SelectedMethods.MajorFactoryMethod, ref value);
            }
            if (methodsFactory.SelectedMethods.TextFactoryMethod.Count > 0)
            {
                _hanlderManager.Handle(text, methodsFactory.SelectedMethods.TextFactoryMethod, ref value);
            }
        }
    }
}
