using Analytics.Shared.Analytics;

namespace Analytics.Shared.Methods
{
    public class MethodsStorage
    {
        public MethodsStorage()
        {
            StringsMethodsInfos = new List<StringMethodInfo>();
            RegularsMethodsInfos = new List<RegularMethodInfo>();
        }

        public IList<StringMethodInfo> StringsMethodsInfos { get; }

        public IList<RegularMethodInfo> RegularsMethodsInfos { get; }
    }
}
