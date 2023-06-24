using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers.Handlers
{
    public class TextFactoryEqualsHandler : BaseHandler<EqualsResult, TextFactoryMethodInfo>
    {
        public TextFactoryEqualsHandler(IMethodsList methodsList) : base(methodsList)
        {
        }

        public override void Handle(string text, IEnumerable<TextFactoryMethodInfo> funks, EqualsResult refResult)
        {
            foreach (var item in funks)
            {
                var methodInfo = new ExtendedMethodInfo();

                try
                {
                    methodInfo.MethodName = item.MethodName;
                    methodInfo.IsEqual = item.Func(text, (string[])item.Arguments);
                    methodInfo.Arguments = item.Arguments;
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
