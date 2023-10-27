using Analytics.Shared.Methods;

namespace Analytics.Shared.Analytics
{
    public class StringMethodInfo : MethodInfo
    {
        public StringMethodInfo(string methodName, IEnumerable<string> arguments, Func<string, string[], bool> func) : base(methodName)
        {
            Arguments = arguments;
            Func = func;
        }

        public IEnumerable<string> Arguments { get; }

        public Func<string, string[], bool> Func { get; }
    }
}
