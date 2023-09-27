using CSharpEFCoreExample;
using CSharpEFCoreExample.ExampleBase;

namespace CSharpEFCoreExampleTests.Examples
{
    public class Example01AInitialSolution : Example01Base
    {
        protected override void Main()
        {
            // arrange
            var discoutService = new DiscoutService(wr);
            var startDate = DateTime.Now.AddDays(-365);
            var products = wr.Db.Products.ToList();
            var productCodes = products.Select(x => x.Code).ToList();
            var customers = wr.Db.Customers.ToList();

            // act
            wr.LogMethod(() => discoutService.Process(startDate, productCodes));
        }
    }
}
