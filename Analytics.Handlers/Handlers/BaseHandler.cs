using Analytics.Methods;

namespace Analytics.Handlers.Handlers
{
    public abstract class BaseHandler<RT, FT>
    {
        protected IMethodsList _methodsList;

        public Type ReturnType => typeof(RT);

        public Type FunctionType => typeof(FT);

        public BaseHandler(IMethodsList methodsList)
        {
            _methodsList = methodsList ?? throw new ArgumentNullException(nameof(methodsList));
        }

        public abstract void Handle(string text, IEnumerable<FT> funks, ref RT result);
    }
}
