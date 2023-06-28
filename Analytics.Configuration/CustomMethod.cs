namespace Analytics.Configuration
{
    public class CustomMethod
    {
        public string MethodName { get; set; } = Guid.NewGuid().ToString();

        public Func<string, bool>? MajorFunc { get; set; }

        public Func<string, string[], bool>? ArgumentsFunc { get; set; }
    }
}
