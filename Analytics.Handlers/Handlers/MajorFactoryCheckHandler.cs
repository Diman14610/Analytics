using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers.Handlers
{
    public class MajorFactoryCheckHandler : BaseHandler<CheckResult, MajorFactoryMethodInfo>
    {
        public MajorFactoryCheckHandler(IMethodsList methodsList) : base(methodsList)
        {
        }

        public override CheckResult Handle(string text, IEnumerable<MajorFactoryMethodInfo> funks)
        {
            var result = new List<ExtendedMethodInfo>();

            foreach (var item in funks)
            {
                var check = new ExtendedMethodInfo()
                {
                    MethodName = item.MethodName,
                };

                try
                {
                    check.IsEqual = item.Func(text);
                }
                catch (Exception ex)
                {
                    check.IsError = true;
                    check.Exception = ex;
                }

                result.Add(check);
            }

            return new CheckResult() { TextMethodInfos = result };
        }
    }
}
