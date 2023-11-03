using Analytics.Handlers.Abstractions.AnalyticsResulCounter;
using Analytics.Handlers.Abstractions.ResultHandler;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Handlers;

namespace Analytics.Handlers.Implementations.ResultHandler
{
    public class AnalyticsResultHandler : IResultHandler<AnalyticsResult>
    {
        private readonly IAnalyticsResultCounter _analyticsResultHandler;

        public AnalyticsResultHandler(IAnalyticsResultCounter analyticsResultHandler)
        {
            _analyticsResultHandler = analyticsResultHandler ?? throw new ArgumentNullException(nameof(analyticsResultHandler));
        }

        public ResultData HandleResult(AnalyticsResult result)
        {
            var counter = new ResultCounter();

            _analyticsResultHandler.Handle(result, counter);

            var resultData = new ResultData(counter.NumberBlocks, counter.NumberMethods, counter.NumberSuccessfulBlocks, counter.NumberSuccessfulMethods);

            return resultData;
        }
    }
}
