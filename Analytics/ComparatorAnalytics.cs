using Analytics.Core;
using Analytics.Shared.Analytics.Comparator;
using Analytics.Shared.Core.Assertion;

namespace Analytics
{
    public class ComparatorAnalytics
    {
        protected AssertionBlock _assertionBlock = new();
        protected ComparatorSettings _comparatorSettings;

        public ComparatorAnalytics(ComparatorSettings? comparatorSettings = null)
        {
            _comparatorSettings = comparatorSettings ?? new ComparatorSettings();
        }

        public ComparatorAnalytics AddBlock(AssertionBlock assertionBlock)
        {
            _assertionBlock = assertionBlock;
            return this;
        }

        public async Task<ComparisonResult> GetBestMatch(string text)
        {
            AssertionResult[] assertionsResults = await _assertionBlock.Proccess(text).ConfigureAwait(false);

            var isBlocksSelected = _comparatorSettings.ComparisonPriority == ComparisonPriority.NumberSuccessfulBlocks;

            AssertionResult? firstSortedAssertion = assertionsResults
                .OrderByDescending(assertion => isBlocksSelected ? assertion.NumberSuccessfulBlocks : assertion.Weight)
                .ThenByDescending(assertion => isBlocksSelected ? assertion.Weight : assertion.NumberSuccessfulBlocks)
                .FirstOrDefault();

            if (firstSortedAssertion == null)
            {
                return new ComparisonResult(string.Empty, 0, 0, _comparatorSettings.ComparisonPriority);
            }

            return new ComparisonResult(
                firstSortedAssertion.Name,
                firstSortedAssertion.Score,
                firstSortedAssertion.NumberSuccessfulBlocks,
                _comparatorSettings.ComparisonPriority
                );
        }
    }
}
