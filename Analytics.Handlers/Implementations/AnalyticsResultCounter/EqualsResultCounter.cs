using Analytics.Handlers.Abstractions.AnalyticsResulCounter;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Handlers;

namespace Analytics.Handlers.Implementations.AnalyticsResultCounter
{
    public class EqualsResultCounter : IAnalyticsResultCounter
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
