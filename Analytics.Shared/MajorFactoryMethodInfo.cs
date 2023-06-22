namespace Analytics.Shared
{
    public class MajorFactoryMethodInfo : FactoryMethodInfo
    {
        public MajorFactoryMethodInfo(string methodName, Func<string, bool> func) : base(methodName)
        {
            Func = func;
        }

        public Func<string, bool> Func { get; }
    }
}
