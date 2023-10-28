using Analytics.Handlers.Abstractions.AnalyticsResultHandler;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Handlers;

namespace Analytics.Handlers.Handlers.ResultHandler
{
    public class EqualsResultHandler : IAnalyticsResultHandler
    {
        public void Handle(AnalyticsResult result, ResultCounter counter)
        {
            if (result.EqualsResult.Count == 0) return;

            counter.NumberBlocks += result.EqualsResult.Count;

            foreach (var equalsResult in result.EqualsResult)
            {
                if (equalsResult.IsEqual)
                {
                    counter.NumberSuccessfulBlocks++;
                }

                counter.NumberMethods += equalsResult.ExtendedMethodInfos.Count;
                counter.NumberSuccessfulMethods += equalsResult.ExtendedMethodInfos.Count(g => g.IsEqual);
            }
        }
    }
}
