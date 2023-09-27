using CSharpEFCoreExample;
using CSharpEFCoreExample.ContextAddons;
using CSharpEFCoreExample.Data;
using CSharpEFCoreExample.ExampleBase;
using CSharpEFCoreExampleTests.ExampleBase;

namespace CSharpEFCoreExampleTests.Examples
{
    internal class Example01BadSolution03 : Example01Base
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

            private record struct DiscoutRecord1(Customer Customer, ICollection<Order> Orders);
            public override void Process(DateTime startDate, IEnumerable<string> productCodes)
            {
                var customersQhasDiscout = Db.Customers
                .Where(c => c.CustomerSince <= startDate)
                .Select(c => new DiscoutRecord1(c, c.Orders)).ToList();

                customersQhasDiscout.ForEach(cQo =>
                {
                    if (cQo.Orders.Any(o => productCodes.Contains(o.Product.Code)))
                    {
                        SendDiscoutToCustomer(cQo.Customer);
                    }
                });
            }
        }
    }
}
