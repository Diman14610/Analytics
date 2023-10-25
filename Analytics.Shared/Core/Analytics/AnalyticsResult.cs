using Analytics.Shared.Analytics;

namespace Analytics.Shared.Core.Analytics
{
    public class AnalyticsResult
    {
        public AnalyticsResult(string? text)
        {
            Text = text;
            CheckResult = new List<CheckResult>();
            EqualsResult = new List<EqualsResult>();
        }

        public string? Text { get; }

        public IList<CheckResult> CheckResult { get; }

        public IList<EqualsResult> EqualsResult { get; }
    }
}
