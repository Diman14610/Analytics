using Analytics.Core;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var r = new BaseAnalytics("#ffffff")
                .CheckFor(
                s => s.Hex,
                s => s.Ip,
                s => s.Int,
                s => s.Str)
                .Analysis();

            Console.ReadKey();
        }
    }
}