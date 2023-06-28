namespace Analytics.Shared.Analytics
{
    public class AnalyticsResult
    {
        public string? Text { get; set; }

        public IList<CheckResult> CheckResult { get; set; } = new List<CheckResult>();

        public IList<EqualsResult> EqualsResult { get; set; } = new List<EqualsResult>();
    }
}
