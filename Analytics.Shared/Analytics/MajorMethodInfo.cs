namespace Analytics.Shared.Analytics
{
    public class MajorMethodInfo : MethodInfo
    {
        public MajorMethodInfo(string methodName, Func<string, bool> func) : base(methodName)
        {
            Func = func;
        }

        public Func<string, bool> Func { get; }
    }
}
