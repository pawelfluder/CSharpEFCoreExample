using CSharpEFCoreExample.ContextAddons;
using CSharpEFCoreExample.Data;

namespace CSharpEFCoreExample
{
    public class DiscoutService
    {
        protected readonly IDbContextWrapper<OrdersDbContext> wr;
        protected OrdersDbContext Db => wr.Db;

        public DiscoutService(IDbContextWrapper<OrdersDbContext> wr)
        {
            this.wr = wr;
        }

        public virtual void Process(DateTime startDate, IEnumerable<string> productCodes)
        {
            var customers = Db.Customers.Where(x => x.CustomerSince >= startDate).ToList();
            foreach (var c in customers)
            {
                if (c.Orders.Any(o => productCodes.Contains(o.Product.Code)))
                {
                    SendDiscoutToCustomer(c);
                }
            }
        }

        protected void SendDiscoutToCustomer(Customer customer)
        {
            wr.LogText(customer.ToString() +
                " received discount");
        }
    }
}