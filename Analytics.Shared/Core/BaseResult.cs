namespace Analytics.Shared.Core
{
    public class BaseResult
    {
        public bool IsEqual { get; set; }

        public bool IsError { get; set; }

        public Exception? Exception { get; set; }
    }
}
