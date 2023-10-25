using Analytics.Shared.Analytics;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Core.Assertion;

namespace Analytics.Core
{
    public abstract class BaseAssertion
    {
        protected virtual AssertionResult Explore(AnalyticsResult analyticsResult, AssertionSettings settings)
        {
            var numberBlocks = 0;
            var numberMethods = 0;

            var numberSuccessfulBlocks = 0;
            var numberSuccessfulMethods = 0;

            if (analyticsResult.CheckResult.Any())
            {
                HandleCheckResult(
                    analyticsResult.CheckResult,
                    ref numberBlocks,
                    ref numberSuccessfulBlocks,
                    ref numberSuccessfulMethods,
                    ref numberMethods
                    );
            }
            if (analyticsResult.EqualsResult.Any())
            {
                HandleEqualsResult(
                    analyticsResult.EqualsResult,
                    ref numberBlocks,
                    ref numberSuccessfulBlocks,
                    ref numberSuccessfulMethods,
                    ref numberMethods
                    );
            }

            var score = settings.WeightFunc == null ? numberSuccessfulBlocks * settings.Weight : settings.WeightFunc(numberSuccessfulBlocks, settings);

            var assertionResult = new AssertionResult(
                settings.Name,
                settings.Weight,
                score,
                numberMethods,
                numberSuccessfulMethods,
                numberBlocks,
                numberSuccessfulBlocks
                );

            return assertionResult;
        }

        protected void HandleCheckResult(
            IList<CheckResult> checkResults,
            ref int numberBlocks,
            ref int numberSuccessfulBlocks,
            ref int numberSuccessfulMethods,
            ref int numberMethods)
        {
            numberBlocks += checkResults.Count;

            foreach (var block in checkResults)
            {
                var isAll = block.ExtendedMethodInfos.All(r => r.IsEqual);

                if (isAll)
                {
                    numberSuccessfulBlocks++;
                }

                foreach (var method in block.ExtendedMethodInfos)
                {
                    if (method.IsEqual)
                    {
                        numberSuccessfulMethods++;
                    }
                    numberMethods++;
                }
            }
        }

        protected void HandleEqualsResult(
            IList<EqualsResult> equalsResults,
            ref int numberBlocks,
            ref int numberSuccessfulBlocks,
            ref int numberSuccessfulMethods,
            ref int numberMethods)
        {
            numberBlocks += equalsResults.Count;

            foreach (var equalsResult in equalsResults)
            {
                if (equalsResult.IsEqual)
                {
                    numberSuccessfulBlocks++;
                }

                numberMethods += equalsResult.ExtendedMethodInfos.Count;
                numberSuccessfulMethods += equalsResult.ExtendedMethodInfos.Count(g => g.IsEqual);
            }
        }
    }
}
