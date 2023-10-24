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
                var a = factory.Analysis(text);

                if (a.CheckResult.Any())
                {
                    var t = a.CheckResult.SelectMany(s => s.ExtendedMethodInfos.Select(r => r.IsEqual)).ToList();
                }
                if (a.EqualsResult.Any())
                {
                    var numberSuccessfulBlocks = 0;
                    var numberBlocks = a.EqualsResult.Count;

                    var numberMethods = 0;
                    var numberSuccessfulMethods = 0;

                    foreach (var item in a.EqualsResult)
                    {
                        if (item.IsEqual)
                        {
                            numberSuccessfulBlocks++;
                        }

                        numberMethods += item.ExtendedMethodInfos.Count;
                        numberSuccessfulMethods += item.ExtendedMethodInfos.Count(g => g.IsEqual);
                    }

                    var score = numberSuccessfulBlocks * settings.Weight;

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

                    //var blocks = a.EqualsResult.Select(s => s.IsEqual).ToList();

                    //var countSuccessfullBlocks = blocks.Count(a => a);
                    //var numberSuccessfulBlocks = blocks.Where(a => a).Count();
                    //var numberBlocks = blocks.Count;

                    //var methods = a.EqualsResult
                    //    .Select(s => s.ExtendedMethodInfos.Select(g => g.IsEqual).ToList())
                    //    .ToList();
                    //var numberMethods = methods.Sum(g => g.Count);
                    //var numberSuccessfulMethods = methods.Sum(g => g.Count(h => h));

                    //var score = countSuccessfullBlocks * settings.Weight;

                    //var assertionResult = new AssertionResult(
                    //    settings.Name,
                    //    settings.Weight,
                    //    score,
                    //    numberMethods,
                    //    numberSuccessfulMethods,
                    //    numberBlocks,
                    //    numberSuccessfulBlocks
                    //    );

                    //result.Add(assertionResult);
                }
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

        public Func<double, double>? WeightFunc { get; set; }
    }
}
