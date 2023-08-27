using Microsoft.EntityFrameworkCore;

namespace CSharpEFCoreExample
{
    public class DiscoutService
    {
        private readonly OrdersDbContext _dbContext;

        public DiscoutService(OrdersDbContext dbContext) => _dbContext = dbContext;
        
        public void Process(DateTime startDate, IEnumerable<string> productCodes)
        {
            EfInterceptor.Log(() =>
                InitialSolution(startDate, productCodes));
            EfInterceptor.Log(() =>
                BadSolution01(startDate, productCodes));
            EfInterceptor.Log(() =>
                BadSolution02(startDate, productCodes));
            EfInterceptor.Log(() =>
                CorrectSolution(startDate, productCodes));

            EfInterceptor.PrintLogToConsole();
            EfInterceptor.PrintLogToPdf();
        }

        private record struct DiscoutRecord1(Customer Customer, ICollection<Order> Orders);
        private void BadSolution01(DateTime startDate, IEnumerable<string> productCodes)
        {
            var customersQhasDiscout = _dbContext.Customers
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

        private record struct DiscoutRecord2(Customer Customer, bool Discout);
        private void BadSolution02(DateTime startDate, IEnumerable<string> productCodes)
        {
            var customersQhasDiscout = _dbContext.Customers
                .Where(c => c.CustomerSince <= startDate)
                .Select(c => new DiscoutRecord2(c,
                    c.Orders.Any(o => productCodes.Contains(o.Product.Code)))).ToList();

            customersQhasDiscout.ForEach(cQd =>
            {
                if (cQd.Discout)
                {
                    SendDiscoutToCustomer(cQd.Customer);
                }
            });
        }

        public void CorrectSolution(DateTime startDate, IEnumerable<string> productCodes)
        {
            var customers = _dbContext.Customers
                .Include(c => c.Orders)
                .Where(c => c.CustomerSince <= startDate)
                .Where(c => c.Orders
                    .Any(o => productCodes.Contains(o.Product.Code))).ToList();

            customers.ForEach(c => SendDiscoutToCustomer(c));
        }

        public void InitialSolution(DateTime startDate, IEnumerable<string> productCodes)
        {
            var customers = _dbContext.Customers.Where(x => x.CustomerSince >= startDate).ToList();
            foreach (var c in customers)
            {
                if (c.Orders.Any(o => productCodes.Contains(o.Product.Code)))
                {
                    SendDiscoutToCustomer(c);
                }
            }
        }

        private void SendDiscoutToCustomer(Customer customer)
        {
            // send a discout to a customer
        }
    }
}