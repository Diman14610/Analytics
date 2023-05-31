namespace Analytics.Core
{
    public class BaseResult
    {
        public bool IsEqual { get; internal set; }

        public bool IsError { get; internal set; }

        public Exception? Exception { get; internal set; }
    }
}
