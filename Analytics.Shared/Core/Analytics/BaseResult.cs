namespace Analytics.Shared.Core.Analytics
{
    public class BaseResult
    {
        public bool IsEqual { get; set; }

        public bool IsError { get; set; }

        public Exception? Exception { get; set; }
    }
}
