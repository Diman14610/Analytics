using Analytics.Handlers.Abstractions.ResultHandler;
using Analytics.Root;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Core.Assertion;
using Analytics.Shared.Handlers;

namespace Analytics.Core.Abstractions
{
    public abstract class BaseAssertion
    {
        private readonly IResultHandler<AnalyticsResult> _resultHandler;

        public BaseAssertion()
        {
            _resultHandler = DefaultDependencies.AnalyticsResultHandler;
        }

        public BaseAssertion(IResultHandler<AnalyticsResult> resultHandler)
        {
            _resultHandler = resultHandler ?? throw new ArgumentNullException(nameof(resultHandler));
        }

        protected virtual AssertionResult Explore(AnalyticsResult analyticsResult, AssertionSettings settings)
        {
            ResultData resultData = _resultHandler.HandleResult(analyticsResult);

            double score = settings.WeightFunc == null ? resultData.NumberSuccessfulBlocks * settings.Weight : settings.WeightFunc(resultData.NumberSuccessfulBlocks, settings);

            var assertionResult = new AssertionResult(settings.Name, settings.Weight, score, resultData.NumberMethods, resultData.NumberSuccessfulMethods, resultData.NumberBlocks, resultData.NumberSuccessfulBlocks);

            return assertionResult;
        }
    }
}
