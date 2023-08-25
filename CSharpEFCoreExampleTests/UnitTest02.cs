using CSharpEFCoreExample;

namespace CSharpEFCoreExampleTests
{
    [TestClass]
    public class UnitTest02 : IDisposable
    {
        private readonly OrdersDbContext db;
        private readonly RandomPropertyGen propertyGen;

        public UnitTest02()
        {
            db = new OrdersDbContext();
            propertyGen = new RandomPropertyGen();
        }

        [TestMethod]
        public void Exercise01()
        {
            var discoutService = new DiscoutService(db);
            var startDate = DateTime.Now.AddDays(-365);
            var products = db.Products.ToList();
            var productCodes = products.Select(x => x.Code).ToList();

            discoutService.Process(startDate, productCodes);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}