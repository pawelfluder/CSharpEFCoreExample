using CSharpEFCoreExample.ContextAddons;

namespace CSharpEFCoreExamples.ExamplesBase.Example01
{
    public abstract class DiscoutService
    {
        protected readonly IDbContextWrapper<OrdersDbContext> wr;
        protected OrdersDbContext Db => wr.Db;

        public DiscoutService(IDbContextWrapper<OrdersDbContext> wr)
        {
            this.wr = wr;
        }

        public abstract void Process(DateTime startDate, IEnumerable<string> productCodes);

        protected void SendDiscoutToCustomer(Customer customer)
        {
            wr.LogText(customer.ToString() +
                " received discount");
        }
    }
}