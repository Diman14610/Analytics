using Analytics;
using Analytics.Core;
using Analytics.Shared.Analytics.Comparator;

namespace TestApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string exampleText = "Dear Mr. Johnson, I hope this letter finds you well. I am writing to inform you about an exciting event happening at our new location in London, UK. Firstly, I wanted to inform you of our new address. Our company, XYZ Corporation, has recently moved to 123 Main Street, London, UK. This modern and spacious facility will allow us to better serve our valued customers like yourself. Speaking of the new location, I would like to invite you to our grand opening event on September 15th, 2022. The event will take place at our state-of-the-art showroom located at 123 Main Street, London, UK. We have prepared a wide range of exciting activities and exclusive offers for our esteemed guests, so please mark your calendar and make sure to join us. During the event, you will have the opportunity to meet our dedicated team, including our CEO, John Smith. Mr. Smith will be giving a speech about the company's vision for the future and the new products we will be launching this year. In addition to the address and location details, I wanted to draw your attention to a special offer we have prepared exclusively for our valued customers. As a token of our appreciation, we are offering a 20% discount on all purchases made during the grand opening event. This discount is valid for one week, so you can take advantage of it if you are unable to attend the event in person. Please let us know if you will be attending the grand opening event by replying to this email or by calling our customer service hotline at +1 (123) 456-7890. We value your presence and look forward to welcoming you. Thank you for your continued support, and we hope to see you at our new location soon! Warm regards, Jennifer Thompson Customer Relations Manager XYZ Corporation";

            var stringComparison = StringComparison.InvariantCultureIgnoreCase;

            var addressBlock = new AnalyticsBlock()
                .Configure(config => config.GlobalStringComparison = stringComparison)
                .EqualsTo(m => m.Contains("street"));

            var personBlock = new AnalyticsBlock()
                .Configure(config => config.GlobalStringComparison = stringComparison)
                .EqualsTo(m => m.Contains("dear", "mr"))
                .EqualsTo(m => m.Contains("johnson"));

            var businessLetter = new AnalyticsBlock()
                .Configure(config => config.GlobalStringComparison = stringComparison)
                .CopyBlocks(personBlock, addressBlock)
                .EqualsTo(m => m.Contains("dear"));

            AssertionBlock assertionBlock = new AssertionBlock()
                .Assert(addressBlock, "Address")
                .Assert(personBlock, "Person")
                .Assert(businessLetter, "Business letter");

            ComparisonResult comparisonResult = await new ComparatorAnalytics()
                .AddBlock(assertionBlock)
                .GetBestMatch(exampleText);

            Console.WriteLine($"Name: {comparisonResult.AssertionName}\nScore: {comparisonResult.Score}\nNumberSuccessfulBlocks: {comparisonResult.NumberSuccessfulBlocks}");
        }
    }
}