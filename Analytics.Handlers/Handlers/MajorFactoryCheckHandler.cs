﻿using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers.Handlers
{
    public class MajorFactoryCheckHandler : BaseHandler<CheckResult, MajorFactoryMethodInfo>
    {
        public MajorFactoryCheckHandler(IMethodsList methodsList) : base(methodsList)
        {
        }

        public override void Handle(string text, IEnumerable<MajorFactoryMethodInfo> funks, CheckResult refResult)
        {
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

                refResult.ExtendedMethodInfos.Add(check);
            }
        }
    }
}
