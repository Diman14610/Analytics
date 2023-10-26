using Analytics.Shared.Analytics;

namespace Analytics.Shared.Core.Analytics
{
    public class AnalyticsResult
    {
        public AnalyticsResult()
        {
            CheckResult = new List<CheckResult>();
            EqualsResult = new List<EqualsResult>();
        }

        public IList<CheckResult> CheckResult { get; }

        public IList<EqualsResult> EqualsResult { get; }
    }
}
