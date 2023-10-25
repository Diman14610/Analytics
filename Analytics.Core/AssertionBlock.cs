using Analytics.Core.Abstractions;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Core.Assertion;

namespace Analytics.Core
{
    public class AssertionBlock : BaseAssertion
    {
        private readonly IList<(AnalyticsBlock, AssertionSettings)> _assertions;

        public AssertionBlock()
        {
            _assertions = new List<(AnalyticsBlock, AssertionSettings)>();
        }

        public AssertionBlock Assert(AnalyticsBlock analyticsBlock, AssertionSettings settings)
        {
            _assertions.Add((analyticsBlock, settings));
            return this;
        }

        public Task<AssertionResult[]> Proccess(string text)
        {
            if (!_assertions.Any())
            {
                throw new ArgumentOutOfRangeException("The number of assertions is zero");
            }

            IEnumerable<Task<AssertionResult>> assertionsTasks = _assertions
                .Select(((AnalyticsBlock factory, AssertionSettings settings) item) => GetAssertionResult(item.factory, item.settings, text));

            return Task.WhenAll(assertionsTasks);
        }

        private Task<AssertionResult> GetAssertionResult(AnalyticsBlock factory, AssertionSettings settings, string text)
        {
            AnalyticsResult analyticsResult = factory.Analysis(text);
            AssertionResult result = Explore(analyticsResult, settings);
            return Task.FromResult(result);
        }
    }
}
