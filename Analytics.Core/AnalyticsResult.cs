namespace Analytics.Core
{
    public class AnalyticsResult
    {
        public string? Text { get; internal set; }

        public ICollection<CheckResult> CheckResult { get; internal set; } = new List<CheckResult>();

        public ICollection<EqualsResult> EqualsResult { get; internal set; } = new List<EqualsResult>();
    }
}
