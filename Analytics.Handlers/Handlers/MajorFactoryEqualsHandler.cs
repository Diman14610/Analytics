using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers.Handlers
{
    public class MajorFactoryEqualsHandler : BaseHandler<EqualsResult, MajorFactoryMethodInfo>
    {
        public MajorFactoryEqualsHandler(IMethodsList methodsList) : base(methodsList)
        {
        }

        public override void Handle(string text, IEnumerable<MajorFactoryMethodInfo> funks, EqualsResult refResult)
        {
            var methodInfo = new ExtendedMethodInfo();

            foreach (var item in funks)
            {
                try
                {
                    methodInfo.MethodName = item.MethodName;
                    methodInfo.IsEqual = item.Func(text);
                }
                catch (Exception ex)
                {
                    refResult.IsError = true;
                    refResult.Exception = ex;
                }

                refResult.ExtendedMethodInfos.Add(methodInfo);
            }
        }
    }
}
