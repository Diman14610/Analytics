namespace Analytics.Shared
{
    public class TextMethodInfo : BaseResult
    {
        public string? MethodName { get; set; }

        public IEnumerable<string>? Arguments { get; set; }
    }
}
