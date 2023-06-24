namespace Analytics.Shared
{
    public class ExtendedMethodInfo : BaseResult
    {
        public string? MethodName { get; set; }

        public IEnumerable<string>? Arguments { get; set; }
    }
}
