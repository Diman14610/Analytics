namespace Analytics.Shared
{
    public class EqualsResult : BaseResult
    {
        public IList<ExtendedMethodInfo> ExtendedMethodInfos { get; set; } = new List<ExtendedMethodInfo>();
    }
}
