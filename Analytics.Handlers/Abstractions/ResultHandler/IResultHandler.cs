using Analytics.Shared.Handlers;

namespace Analytics.Handlers.Abstractions.ResultHandler
{
    public interface IResultHandler<TResult>
    {
        ResultData HandleResult(TResult result);
    }
}
