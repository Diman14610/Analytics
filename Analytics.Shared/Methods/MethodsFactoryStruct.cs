using Analytics.Shared.Analytics;

namespace Analytics.Shared.Methods
{
    public class MethodsFactoryStruct
    {
        public IList<ArgumentsMethodInfo> TextFactoryMethod { get; set; } = new List<ArgumentsMethodInfo>();

        public IList<MajorMethodInfo> MajorFactoryMethod { get; set; } = new List<MajorMethodInfo>();
    }
}
