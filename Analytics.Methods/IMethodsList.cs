namespace Analyzer.Methods
{
    public interface IMethodsList
    {
        Func<string, bool>? TryGetMethod(string name);
    }
}