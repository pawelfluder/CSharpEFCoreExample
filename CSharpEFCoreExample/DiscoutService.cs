using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace CSharpEFCoreExample
{
    public class DiscoutService
    {
        private readonly OrdersDbContext _dbContext;

        public DiscoutService(OrdersDbContext dbContext) => _dbContext = dbContext;

        private record struct DiscoutRecord(Customer customer, ICollection<Order> order);

        private record struct DiscoutRecord2(Customer customer, bool discout);

        public void Process(DateTime startDate, IEnumerable<string> productCodes)
        {
            var allCustomers = _dbContext.Customers.ToList();

            var query09 = _dbContext.Customers
                .Where(c => c.CustomerSince <= startDate)
                .ToList();

            var query06 = _dbContext.Customers
                .Where(c => c.CustomerSince <= startDate)
                .Include(c => c.Orders)
                .ToList();

            var query05 = _dbContext.Customers
                .Include(c => c.Orders)
                .Where(c => c.CustomerSince <= startDate)
                .Where(c => c.Orders
                    .Any(o => productCodes.Contains(o.Product.Code))).ToList();

            

            var query04 = _dbContext.Customers
                .Where(c => c.CustomerSince <= startDate)
                .Select(c => new DiscoutRecord2(c,
                    c.Orders.Any(o => productCodes.Contains(o.Product.Code)))).ToList();

            //var query01 = _dbContext.Customers.Include(x => x.Orders)
            //    .Where(x => x.CustomerSince <= startDate);
            //var queryString01 = query01.ToQueryString();
            //var customers = query01.ToList();

            //foreach (var c in customers)
            //{
            //    var query02 = _dbContext.Orders);
            //    //var queryString02 = query02.ToQueryString();

            //    var query03 = c.Orders.Any(o => productCodes.Contains(o.Product.Code));

            //    if (query02)
            //    {

            //    }
            //}
        }
    }
}