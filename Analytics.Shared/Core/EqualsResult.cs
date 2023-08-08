using Analytics.Shared.Core;

namespace Analytics.Shared.Analytics
{
    public class EqualsResult : BaseResult
    {
        public IList<ExtendedMethodInfo> ExtendedMethodInfos { get; set; } = new List<ExtendedMethodInfo>();
    }
}
