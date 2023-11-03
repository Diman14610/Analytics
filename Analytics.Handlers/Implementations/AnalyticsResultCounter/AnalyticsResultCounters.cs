using Analytics.Handlers.Abstractions.AnalyticsResulCounter;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Handlers;

namespace Analytics.Handlers.Implementations.AnalyticsResultCounter
{
    public class AnalyticsResultCounters : IAnalyticsResultCounter
    {
        private readonly IEnumerable<IAnalyticsResultCounter> _analyticsResultCounters;

        public AnalyticsResultCounters(IEnumerable<IAnalyticsResultCounter> analyticsResultCounters)
        {
            _analyticsResultCounters = analyticsResultCounters ?? throw new ArgumentNullException(nameof(analyticsResultCounters));
        }

        public void Handle(AnalyticsResult result, ResultCounter counter)
        {
            foreach (var resultCounter in _analyticsResultCounters)
            {
                resultCounter.Handle(result, counter);
            }
        }
    }
}
