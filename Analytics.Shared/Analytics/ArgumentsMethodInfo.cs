namespace Analytics.Shared.Analytics
{
    public class ArgumentsMethodInfo : MethodInfo
    {
        public ArgumentsMethodInfo(string methodName, IEnumerable<string> arguments, Func<string, string[], bool> func) : base(methodName)
        {
            Arguments = arguments;
            Func = func;
        }

        public IEnumerable<string> Arguments { get; }

        public Func<string, string[], bool> Func { get; }
    }
}
