namespace Analytics.Shared.Core.Assertion
{
    public class AssertionSettings
    {
        public string Name { get; set; } = Guid.NewGuid().ToString();

        public double Weight { get; set; } = 0.1;

        public Func<int, AssertionSettings, double>? WeightFunc { get; set; }

        public AssertionSettings Copy()
        {
            return new AssertionSettings { Name = Name, Weight = Weight, WeightFunc = WeightFunc };
        }
    }
}
