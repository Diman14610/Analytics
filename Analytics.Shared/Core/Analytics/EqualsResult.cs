namespace Analytics.Shared.Core.Analytics
{
    public class EqualsResult : BaseResult
    {
        public IList<ExtendedMethodInfo> ExtendedMethodInfos { get; } = new List<ExtendedMethodInfo>();
    }
}
