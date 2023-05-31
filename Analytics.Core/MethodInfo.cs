namespace Analytics.Core
{
    public class MethodInfo : BaseResult
    {
        public string? MethodName { get; internal set; }

        public bool IsMethodFound { get; internal set; }
    }
}
