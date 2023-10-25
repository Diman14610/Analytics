using Analytics.Shared.Analytics;

namespace Analytics
{
    public class AssertionBlock
    {
        private readonly IList<(AnalyticsBlock, AssertionSettings)> _factories;

        public AssertionBlock()
        {
            _factories = new List<(AnalyticsBlock, AssertionSettings)>();
        }

        public AssertionBlock Assert(AnalyticsBlock analyticsBlock, AssertionSettings settings)
        {
            _factories.Add((analyticsBlock, settings));
            return this;
        }

        public IEnumerable<AssertionResult> Proccess(string text)
        {
            List<AssertionResult> result = new List<AssertionResult>();

            foreach ((AnalyticsBlock factory, AssertionSettings settings) in _factories)
            {
                var analyticsResult = factory.Analysis(text);

                var numberBlocks = 0;
                var numberMethods = 0;

                var numberSuccessfulBlocks = 0;
                var numberSuccessfulMethods = 0;

                if (analyticsResult.CheckResult.Any())
                {
                    numberBlocks += analyticsResult.CheckResult.Count;

                    foreach (var block in analyticsResult.CheckResult)
                    {
                        var isAll = block.ExtendedMethodInfos.All(r => r.IsEqual);

                        if (isAll)
                        {
                            numberSuccessfulBlocks++;
                        }

                        foreach (var method in block.ExtendedMethodInfos)
                        {
                            if (method.IsEqual)
                            {
                                numberSuccessfulMethods++;
                            }
                            numberMethods++;
                        }
                    }
                }
                if (analyticsResult.EqualsResult.Any())
                {
                    numberBlocks += analyticsResult.EqualsResult.Count;

                    foreach (var equalsResult in analyticsResult.EqualsResult)
                    {
                        if (equalsResult.IsEqual)
                        {
                            numberSuccessfulBlocks++;
                        }

                        numberMethods += equalsResult.ExtendedMethodInfos.Count;
                        numberSuccessfulMethods += equalsResult.ExtendedMethodInfos.Count(g => g.IsEqual);
                    }
                }

                var score = settings.WeightFunc == null ? numberSuccessfulBlocks * settings.Weight : settings.WeightFunc(numberSuccessfulBlocks, settings);

                var assertionResult = new AssertionResult(
                settings.Name,
                settings.Weight,
                score,
                numberMethods,
                numberSuccessfulMethods,
                numberBlocks,
                numberSuccessfulBlocks
                );

                result.Add(assertionResult);
            }

            return result;
        }
    }

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

    public class AssertionSettings
    {

        public string Name { get; set; } = Guid.NewGuid().ToString();

        public double Weight { get; set; }

        public Func<int, AssertionSettings, double>? WeightFunc { get; set; }
    }
}
