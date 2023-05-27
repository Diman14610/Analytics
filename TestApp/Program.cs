using Analyzer.Core;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var r = new BaseAnalyzer("#ffffff")
                .CheckFor(
                s => s.Hex,
                s => s.Ip,
                s => s.Int,
                s => s.Str)
                .Analiz();

            Console.ReadKey();
        }
    }
}