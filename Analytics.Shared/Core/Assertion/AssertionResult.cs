namespace Analytics.Shared.Core.Assertion
{
    public class AssertionResult
    {
        public AssertionResult(string name, double weight, double score, int numberMethods, int numberSuccessfulMethods, int numberBlocks, int numberSuccessfulBlocks)
        {
            Name = name;
            Weight = weight;
            Score = score;
            NumberMethods = numberMethods;
            NumberSuccessfulMethods = numberSuccessfulMethods;
            NumberBlocks = numberBlocks;
            NumberSuccessfulBlocks = numberSuccessfulBlocks;
        }

        public string Name { get; }

        public double Weight { get; }

        public double Score { get; }

        public int NumberMethods { get; }

        public int NumberSuccessfulMethods { get; }

        public int NumberBlocks { get; }

        public int NumberSuccessfulBlocks { get; }
    }
}
