using Analytics.Handlers.Abstractions.AnalyticsResultHandler;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Handlers;

namespace Analytics.Handlers.Handlers.ResultHandler
{
    public class CheckResultHandler : IAnalyticsResultHandler
    {
        public void Handle(AnalyticsResult result, ResultCounter counter)
        {
            if (result.CheckResult.Count == 0) return;

            counter.NumberBlocks += result.CheckResult.Count;

            foreach (var block in result.CheckResult)
            {
                var isAll = block.ExtendedMethodInfos.All(r => r.IsEqual);

                if (isAll)
                {
                    counter.NumberSuccessfulBlocks++;
                }

                foreach (var method in block.ExtendedMethodInfos)
                {
                    if (method.IsEqual)
                    {
                        counter.NumberSuccessfulMethods++;
                    }
                    counter.NumberMethods++;
                }
            }
        }
    }
}
