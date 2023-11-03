using Analytics.Core.Abstractions;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Core.Assertion;

namespace Analytics.Core
{
    public class AssertionBlock : BaseAssertion
    {
        private readonly List<(AnalyticsBlock, AssertionSettings)> _assertions = new();

        public AssertionBlock Assert(AnalyticsBlock analyticsBlock, AssertionSettings settings)
        {
            _assertions.Add((analyticsBlock, settings));
            return this;
        }

        public Task<AssertionResult[]> Proccess(string text)
        {
            if (_assertions.Count == 0)
            {
                throw new ArgumentOutOfRangeException("The number of assertions is zero");
            }

            IEnumerable<Task<AssertionResult>> assertionsTasks = _assertions
                .Select(((AnalyticsBlock analytics, AssertionSettings settings) item) => GetAssertionResult(item.analytics, item.settings, text));

            return Task.WhenAll(assertionsTasks);
        }

        protected Task<AssertionResult> GetAssertionResult(AnalyticsBlock analytics, AssertionSettings settings, string text)
        {
            AnalyticsResult analyticsResult = analytics.Analysis(text);
            AssertionResult assertionResult = Explore(analyticsResult, settings);
            return Task.FromResult(assertionResult);
        }
    }
}
