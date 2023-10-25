using Analytics.Core;
using Analytics.Shared.Analytics;
using Analytics.Shared.Core.Assertion;

namespace Analytics
{
    public class AssertionBlock : BaseAssertion
    {
        private readonly IList<(AnalyticsBlock, AssertionSettings)> _factories;

        public AssertionBlock()
        {
            _factories = new List<(AnalyticsBlock, AssertionSettings)>();
        }

        public AssertionBlock Assert(AnalyticsBlock analyticsBlock, AssertionSettings settings)
        {
            _factories.Add((analyticsBlock, settings));
            return this;
        }

        public Task<AssertionResult[]> Proccess(string text)
        {
            IEnumerable<Task<AssertionResult>> assertionResultTask = _factories.Select(((AnalyticsBlock factory, AssertionSettings settings) item) => GetAssertionResult(item.factory, item.settings, text));
            return Task.WhenAll(assertionResultTask);
        }

        private Task<AssertionResult> GetAssertionResult(AnalyticsBlock factory, AssertionSettings settings, string text)
        {
            AnalyticsResult analyticsResult = factory.Analysis(text);
            AssertionResult result = Explore(analyticsResult, settings);
            return Task.FromResult(result);
        }
    }
}
