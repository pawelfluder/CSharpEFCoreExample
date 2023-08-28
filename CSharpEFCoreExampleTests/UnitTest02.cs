using CSharpEFCoreExample;
using CSharpEFCoreExample.ContextAddons;
using CSharpEFCoreExample.Data;

namespace CSharpEFCoreExampleTests
{
    [TestClass]
    public class UnitTest02 : IDisposable
    {
        private readonly DbContextWrapper<OrdersDbContext> wr;

        public UnitTest02()
        {
            wr = new DbContextWrapper<OrdersDbContext>();
        }

        [TestMethod]
        public void Exercise01()
        {
            var discoutService = new DiscoutService(wr);
            var startDate = DateTime.Now.AddDays(-365);
            var products = wr.Db.Products.ToList();
            var productCodes = products.Select(x => x.Code).ToList();

            discoutService.Process(startDate, productCodes);
        }

        public void Dispose()
        {
            wr.Db.Dispose();
        }
    }
}