﻿using Analytics.Configuration;
using Analytics.Handlers.Abstractions;
using Analytics.Methods;
using Analytics.Root;
using Analytics.Shared.Analytics;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Methods;

namespace Analytics.Core.Abstractions
{
    public abstract class BaseAnalytics
    {
        private readonly IEnumerable<IMethodsStorageHandler> _methodsStorageHandlers;
        private readonly AnalyticsConfigurationProvider _configuration = new();

        public AnalyticsConfiguration Configuration
        {
            get => _configuration;
        }

        public BaseAnalytics()
        {
            _methodsStorageHandlers = DefaultDependencies.MethodsStorageHandlers;
        }

        public BaseAnalytics(IEnumerable<IMethodsStorageHandler> methodsInfosHandlers)
        {
            _methodsStorageHandlers = methodsInfosHandlers ?? throw new ArgumentNullException(nameof(methodsInfosHandlers));
        }

        /// <summary>
        /// Checks the selected methods from the <paramref name="methodsProvider"/> for compliance with the <paramref name="text"/>.
        /// </summary>
        /// <param name="text">Text, sentence, whatever</param>
        /// <param name="methodsProvider">Method factory, to get selected methods for text analysis</param>
        /// <returns>Checked information for each selected method.</returns>
        protected virtual CheckResult CheckFor(string text, MethodsConstructorProvider methodsProvider)
        {
            var checkResult = new CheckResult();

            CallToHandler(text, methodsProvider, checkResult);

            return checkResult;
        }

        /// <summary>
        /// Compares the <paramref name="text"/> with the selected methods from the <paramref name="methodsProvider"/>. Applies logical comparison AND
        /// </summary>
        /// <param name="text">Text, sentence, whatever</param>
        /// <param name="methodsProvider">Method factory, to get selected methods for text analysis</param>
        /// <returns>Returns the result of checking the selected methods according to a logical condition AND. Also additional information about each selected method.</returns>
        protected virtual EqualsResult EqualsTo(string text, MethodsConstructorProvider methodsProvider)
        {
            var equalsResult = new EqualsResult();

            CallToHandler(text, methodsProvider, equalsResult);

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

        protected virtual void CallToHandler<ResultType>(string text, MethodsConstructorProvider methodsProvider, ResultType value)
        {
            MethodsStorage selectedMethods = methodsProvider.GetSelectedMethods();

            foreach (IMethodsStorageHandler methodsStorageHandler in _methodsStorageHandlers)
            {
                methodsStorageHandler.Handle(text, selectedMethods, ref value);
            }
        }
    }
}
