﻿using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers.Handlers
{
    public class EqualsHandler : BaseHandler<EqualsResult>
    {
        public EqualsHandler(IMethodsList methodsList) : base(methodsList)
        {
        }

        public override EqualsResult Handle(IEnumerable<string> methods, string text)
        {
            var result = new EqualsResult()
            {
                Methods = methods,
            };

            try
            {
                IEnumerable<bool> matchesWithText = methods.Select(method =>
                {
                    Func<string, bool>? gettedMethod = _methodsList.TryGetMethod(method);

                    if (gettedMethod == null) return false;

                    return gettedMethod(text);
                });

                result.IsEqual = matchesWithText.All(m => m);
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Exception = ex;
            }

            return result;
        }

        public override EqualsResult Handle(string text, IEnumerable<(string[] strings, Func<string, string[], bool> func)> funks)
        {
            throw new NotImplementedException();
        }
    }
}
