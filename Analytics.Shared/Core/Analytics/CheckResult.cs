using Analytics.Shared.Core.Analytics;

namespace Analytics.Shared.Analytics
{
    public class CheckResult
    {
        public IList<ExtendedMethodInfo> ExtendedMethodInfos { get; } = new List<ExtendedMethodInfo>();
    }
}
