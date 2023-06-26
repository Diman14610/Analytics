using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers.Handlers
{
    public class TextFactoryCheckHandler : BaseHandler<CheckResult, TextFactoryMethodInfo>
    {
        public TextFactoryCheckHandler(IMethodsList methodsList) : base(methodsList)
        {
        }

        public override void Handle(string text, IEnumerable<TextFactoryMethodInfo> funks, ref CheckResult result)
        {
            foreach (var item in funks)
            {
                var check = new ExtendedMethodInfo()
                {
                    MethodName = item.MethodName,
                    IsEqual = item.Func(text, (string[])item.Arguments),
                };

                try
                {
                    check.IsEqual = item.Func(text, (string[])item.Arguments);
                    check.Arguments = item.Arguments;
                }
                catch (Exception ex)
                {
                    check.IsError = true;
                    check.Exception = ex;
                }

                result.ExtendedMethodInfos.Add(check);
            }
        }
    }
}
