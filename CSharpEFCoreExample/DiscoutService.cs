using Microsoft.EntityFrameworkCore;

namespace CSharpEFCoreExample
{
    public class DiscoutService
    {
        private readonly OrdersDbContext db;

        public DiscoutService(OrdersDbContext dbContext) => db = dbContext;
        
        public void Process(DateTime startDate, IEnumerable<string> productCodes)
        {
            db.LogMethod(() =>
                InitialSolution(startDate, productCodes));
            db.LogMethod(() =>
                BadSolution01(startDate, productCodes));
            db.LogMethod(() =>
                BadSolution02(startDate, productCodes));
            db.LogMethod(() =>
                BadSolution03(startDate, productCodes));
            db.LogMethod(() =>
                BadSolution04(startDate, productCodes));
            db.LogMethod(() =>
                CorrectSolution(startDate, productCodes));

            db.PrintLogsToConsole();
        }

        public void InitialSolution(DateTime startDate, IEnumerable<string> productCodes)
        {
            var customers = db.Customers.Where(x => x.CustomerSince >= startDate).ToList();
            foreach (var c in customers)
            {
                if (c.Orders.Any(o => productCodes.Contains(o.Product.Code)))
                {
                    SendDiscoutToCustomer(c);
                }
            }
        }

        public void BadSolution01(DateTime startDate, IEnumerable<string> productCodes)
        {
            var customers = db.Customers
                .Include(x => x.Orders)
                .Where(x => x.CustomerSince >= startDate).ToList();
            foreach (var c in customers)
            {
                if (c.Orders.Any(o => productCodes.Contains(o.Product.Code)))
                {
                    SendDiscoutToCustomer(c);
                }
            }
        }

        public void BadSolution02(DateTime startDate, IEnumerable<string> productCodes)
        {
            var customers = db.Customers
                .Where(x => x.CustomerSince >= startDate).ToList();
            foreach (var c in customers)
            {
                if (db.Orders.Any(o => productCodes.Contains(o.Product.Code)))
                {
                    SendDiscoutToCustomer(c);
                }
            }
        }

        private record struct DiscoutRecord1(Customer Customer, ICollection<Order> Orders);
        private void BadSolution03(DateTime startDate, IEnumerable<string> productCodes)
        {
            var customersQhasDiscout = db.Customers
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
        private void BadSolution04(DateTime startDate, IEnumerable<string> productCodes)
        {
            var customersQhasDiscout = db.Customers
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
            var customers = db.Customers
                .Include(c => c.Orders)
                .Where(c => c.CustomerSince <= startDate)
                .Where(c => c.Orders
                    .Any(o => productCodes.Contains(o.Product.Code))).ToList();

            customers.ForEach(c => SendDiscoutToCustomer(c));
        }

        private void SendDiscoutToCustomer(Customer customer)
        {
            // send a discout to a customer
        }
    }
}