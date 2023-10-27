namespace Analytics.Shared.Methods
{
    public class RegularMethodInfo : MethodInfo
    {
        public RegularMethodInfo(string methodName, Func<string, bool> func) : base(methodName)
        {
            Func = func;
        }

        public Func<string, bool> Func { get; }
    }
}
