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

        /// <summary>
        /// Checks the selected methods from the <paramref name="methodsFactory"/> for compliance with the <paramref name="text"/>.
        /// </summary>
        /// <param name="text">Text, sentence, whatever</param>
        /// <param name="methodsFactory">Method factory, to get selected methods for text analysis</param>
        /// <returns>Checked information for each selected method.</returns>
        protected CheckResult CheckFor(string text, MethodsFactory methodsFactory)
        {
            var checkResult = new CheckResult();

            CallToHandler(text, methodsFactory, checkResult);

            return checkResult;
        }

        /// <summary>
        /// Compares the <paramref name="text"/> with the selected methods from the <paramref name="methodsFactory"/>. Applies logical comparison AND
        /// </summary>
        /// <param name="text">Text, sentence, whatever</param>
        /// <param name="methodsFactory">Method factory, to get selected methods for text analysis</param>
        /// <returns>Returns the result of checking the selected methods according to a logical condition AND. Also additional information about each selected method.</returns>
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
