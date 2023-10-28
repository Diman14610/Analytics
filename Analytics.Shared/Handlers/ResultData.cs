namespace Analytics.Shared.Handlers
{
    public class ResultData
    {
        public ResultData(int numberBlocks, int numberMethods, int numberSuccessfulBlocks, int numberSuccessfulMethods)
        {
            NumberBlocks = numberBlocks;
            NumberMethods = numberMethods;
            NumberSuccessfulBlocks = numberSuccessfulBlocks;
            NumberSuccessfulMethods = numberSuccessfulMethods;
        }

        public int NumberBlocks { get; }

        public int NumberMethods { get; }

        public int NumberSuccessfulBlocks { get; }

        public int NumberSuccessfulMethods { get; }
    }
}
