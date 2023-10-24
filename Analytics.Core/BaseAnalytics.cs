using Analytics.Configuration;
using Analytics.Handlers;
using Analytics.Methods;
using Analytics.Root;
using Analytics.Shared.Analytics;
using Analytics.Shared.Methods;

namespace Analytics.Core
{
    public abstract class BaseAnalytics
    {
        private readonly AnalyticsConfigurationProvider _configuration;
        protected readonly IHandlersManager _handlersManager;

        public AnalyticsConfiguration Configuration
        {
            get => _configuration;
        }

        public BaseAnalytics()
        {
            _handlersManager = DefaultDependencies.GetHandlersManager();
            _configuration = DefaultDependencies.GetAnalyticsConfigurationProvider();
        }

        /// <summary>
        /// Checks the selected methods from the <paramref name="methodsFactory"/> for compliance with the <paramref name="text"/>.
        /// </summary>
        /// <param name="text">Text, sentence, whatever</param>
        /// <param name="methodsFactory">Method factory, to get selected methods for text analysis</param>
        /// <returns>Checked information for each selected method.</returns>
        protected virtual CheckResult CheckFor(string text, MethodsConstructorProvider methodsFactory)
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
        protected virtual EqualsResult EqualsTo(string text, MethodsConstructorProvider methodsFactory)
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

        protected virtual void CallToHandler<ResultType>(string text, MethodsConstructorProvider methodsFactory, ResultType value)
        {
            MethodsStruct selectedMethods = methodsFactory.GetSelectedMethods();

            if (selectedMethods.MajorFactoryMethod.Count > 0)
            {
                _handlersManager.Handle(text, selectedMethods.MajorFactoryMethod, ref value);
            }
            if (selectedMethods.TextFactoryMethod.Count > 0)
            {
                _handlersManager.Handle(text, selectedMethods.TextFactoryMethod, ref value);
            }
        }
    }
}
