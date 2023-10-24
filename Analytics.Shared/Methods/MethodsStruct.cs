using Analytics.Shared.Analytics;

namespace Analytics.Shared.Methods
{
    public class MethodsStruct
    {
        public MethodsStruct()
        {
            TextFactoryMethod = new List<ArgumentsMethodInfo>();
            MajorFactoryMethod = new List<MajorMethodInfo>();
        }

        public IList<ArgumentsMethodInfo> TextFactoryMethod { get; } 

        public IList<MajorMethodInfo> MajorFactoryMethod { get; } 
    }
}
