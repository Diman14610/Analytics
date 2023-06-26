using Analytics.Methods;

namespace Analytics.Handlers.Handlers
{
    public abstract class BaseHandler<RT, FT>
    {
        public Type ReturnType => typeof(RT);

        public Type FunctionType => typeof(FT);

        public abstract void Handle(string text, IEnumerable<FT> funks, ref RT result);
    }
}
