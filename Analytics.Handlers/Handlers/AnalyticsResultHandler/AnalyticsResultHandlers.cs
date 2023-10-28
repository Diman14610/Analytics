using Analytics.Handlers.Abstractions.AnalyticsResultHandler;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Handlers;

namespace Analytics.Handlers.Handlers.AnalyticsResultHandler
{
    public class AnalyticsResultHandlers : IAnalyticsResultHandler
    {
        private readonly IEnumerable<IAnalyticsResultHandler> _analyticsResultHandlers;

        public AnalyticsResultHandlers(IEnumerable<IAnalyticsResultHandler> analyticsResultHandlers)
        {
            _analyticsResultHandlers = analyticsResultHandlers ?? throw new ArgumentNullException(nameof(analyticsResultHandlers));
        }

        public void Handle(AnalyticsResult result, ResultCounter counter)
        {
            foreach (var handler in _analyticsResultHandlers)
            {
                handler.Handle(result, counter);
            }
        }
    }
}
