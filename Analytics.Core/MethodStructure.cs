namespace Analytics.Core
{
    public class MethodStructure
    {
        public MethodStructure(Type type, IEnumerable<string> methods)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Methods = methods ?? throw new ArgumentNullException(nameof(methods));
        }

        public Type Type { get; set; }

        public IEnumerable<string> Methods { get; set; }
    }
}