namespace Analytics.Methods
{
    public interface IMethodsList
    {
        Func<string, bool>? TryGetMethod(string name);
    }
}