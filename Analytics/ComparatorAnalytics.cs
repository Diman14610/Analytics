using Analytics.Configuration;
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

        public ComparatorAnalytics AddBlock(AnalyticsBlock block)
        {
            AssertionSettings? assertionSettings = ((AnalyticsConfigurationProvider)block.Configuration).GetAssertionSettings();

            if (assertionSettings == null)
            {
                return this;
            }

            _assertionBlock.Assert(block, assertionSettings);

            return this;
        }

        public ComparatorAnalytics AddBlocks(IEnumerable<AnalyticsBlock> blocks)
        {
            foreach (var block in blocks)
            {
                AddBlock(block);
            }
            return this;
        }

        public async Task<ComparatorResult> GetBestMatch(string text)
        {
            AssertionResult[] assertionsResults = await _assertionBlock.Proccess(text).ConfigureAwait(false);

            var isNumberSuccessfulBlocks = _comparatorSettings.ComparisonPriority == ComparisonPriority.NumberSuccessfulBlocks;

            AssertionResult? firstSortedAssertion = assertionsResults
                .OrderByDescending(assertion => isNumberSuccessfulBlocks ? assertion.NumberSuccessfulMethods : assertion.Weight)
                .ThenByDescending(assertion => isNumberSuccessfulBlocks ? assertion.Weight : assertion.NumberSuccessfulMethods)
                .FirstOrDefault();

            if (firstSortedAssertion == null)
            {
                return new ComparatorResult(string.Empty, 0, 0, _comparatorSettings.ComparisonPriority);
            }

            return new ComparatorResult(
                firstSortedAssertion.Name,
                firstSortedAssertion.Score,
                firstSortedAssertion.NumberSuccessfulBlocks,
                _comparatorSettings.ComparisonPriority
                );
        }
    }
}
