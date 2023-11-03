using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Handlers;

namespace Analytics.Handlers.Abstractions.AnalyticsResulCounter
{
    public interface IAnalyticsResultCounter
    {
        void Handle(AnalyticsResult result, ResultCounter counter);
    }
}
