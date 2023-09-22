using Analytics.Shared.Analytics;

namespace Analytics.Shared.Methods
{
    public class MethodsFactoryStruct
    {
        public MethodsFactoryStruct()
        {
            TextFactoryMethod = new List<ArgumentsMethodInfo>();
            MajorFactoryMethod = new List<MajorMethodInfo>();
        }

        public IList<ArgumentsMethodInfo> TextFactoryMethod { get; } 

        public IList<MajorMethodInfo> MajorFactoryMethod { get; } 
    }
}
