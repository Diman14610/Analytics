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
                    var t2 = a.EqualsResult.Select(s => s.IsEqual).ToList();
                }
            }

            return result;
        }
    }

    public class AssertionResult
    {
        public AssertionResult(string name, double weight, double sum, int numberProcessedMethods, int numberSuccessfulMethods)
        {
            Name = name;
            Weight = weight;
            Sum = sum;
            NumberProcessedMethods = numberProcessedMethods;
            NumberSuccessfulMethods = numberSuccessfulMethods;
        }

        public string Name { get; }

        public double Weight { get; }

        public double Sum { get; }

        public int NumberProcessedMethods { get; }

        public int NumberSuccessfulMethods { get; }
    }

    public class AssertionSettings
    {

        public string Name { get; set; } = Guid.NewGuid().ToString();

        public double Weight { get; set; }

        public Func<double, double>? WeightFunc { get; set; }
    }
}
