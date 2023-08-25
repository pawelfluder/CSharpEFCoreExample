using Microsoft.EntityFrameworkCore;

namespace CSharpEFCoreExample
{
    internal class DiscoutService
    {
        private readonly OrdersDbContext _dbContext;

        public DiscoutService(OrdersDbContext dbContext) => _dbContext = dbContext;

        public void Process(DateTime startDate, IEnumerable<string> productCodes)
        {
            var query03 = _dbContext.Customers.ToList();

            var query01 = _dbContext.Customers.Where(x => x.CustomerSince >= startDate);
            var queryString01 = query01.ToQueryString();
            var customers = query01.ToList();

            foreach (var c in customers)
            {
                var query02 = c.Orders.Any(o => productCodes.Contains(o.Product.Code));
                //var queryString02 = query02.ToQueryString();
                if (query02)
                {

                }
            }
        }
    }
}