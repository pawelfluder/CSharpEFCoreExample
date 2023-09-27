using CSharpEFCoreExample;
using CSharpEFCoreExample.ContextAddons;
using CSharpEFCoreExample.Data;
using CSharpEFCoreExample.ExampleBase;
using Microsoft.EntityFrameworkCore;

namespace CSharpEFCoreExampleTests.Examples
{
    internal class Example01BadSolution02 : Example01Base
    {
        protected override void Main()
        {
            // arrange
            var discoutService = new NewDiscoutService(wr);
            var startDate = DateTime.Now.AddDays(-365);
            var products = wr.Db.Products.ToList();
            var productCodes = products.Select(x => x.Code).ToList();
            var customers = wr.Db.Customers.ToList();

            // act
            wr.LogMethod(() => discoutService.Process(startDate, productCodes));
        }

        public class NewDiscoutService : DiscoutService
        {
            public NewDiscoutService(IDbContextWrapper<OrdersDbContext> wr)
                : base(wr)
            { }

            public override void Process(DateTime startDate, IEnumerable<string> productCodes)
            {
                var customers = Db.Customers
                    .Where(x => x.CustomerSince <= startDate).ToList();

                Db.Orders.Load<Order>();

                foreach (var c in customers)
                {
                    if (c.Orders.Any(o => productCodes.Contains(o.Product.Code)))
                    {
                        SendDiscoutToCustomer(c);
                    }
                }
            }
        }
    }
}
