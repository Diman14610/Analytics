using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Handlers;

namespace Analytics.Handlers.Abstractions.AnalyticsResultHandler
{
    public interface IAnalyticsResultHandler
    {
        void Handle(AnalyticsResult result, ResultCounter counter);
    }
}
