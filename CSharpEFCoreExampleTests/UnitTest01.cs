using CSharpEFCoreExample;
using Microsoft.EntityFrameworkCore;

namespace CSharpEFCoreExampleTests
{
    [TestClass]
    public class UnitTest01 : IDisposable
    {
        private readonly OrdersDbContext db;
        private readonly RandomPropertyGen propertyGen;

        public DateTime[] JoinDates { get; }
        public DateTime[] BithDates { get; }

        public UnitTest01()
        {
            db = new OrdersDbContext();
            propertyGen = new RandomPropertyGen();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        [DataRow(5)]
        [TestMethod]
        public void CompletlyNewData(int max)
        {
            DeleteAllRows(db.Customers);
            DeleteAllRows(db.Products);
            DeleteAllRows(db.Orders);

            CreateFewProducts(max);
            CreateFewCustomers(max);
            CreateFewOrders();
        }

        [DataRow(5)]
        [TestMethod]
        public void CreateFewCustomers(int max)
        {
            // arrange
            var customers = db.Customers.ToList();

            // act
            for (int i = 0; i < max; i++)
            {
                CreateCustomer();
            }

            // assert
            var updatedCustomers = db.Customers.ToList();
            Assert.AreEqual(customers.Count + max, updatedCustomers.Count);
        }

        [DataRow(5)]
        [TestMethod]
        public void CreateFewProducts(int max)
        {
            // arrange
            // act
            for (int i = 0; i < max; i++)
            {
                CreateProduct();
            }
            // assert
        }

        [TestMethod]
        public void CreateProduct()
        {
            // arrange
            var newProduct = propertyGen.Product();

            // act
            var products = db.Products;
            products.Add(newProduct);
            db.SaveChanges();

            // assert
        }

        [TestMethod]
        public void CreateFewOrders()
        {
            // arrange
            var customers = db.Customers.ToList();

            // act
            foreach (var cm in customers)
            {
                var order = propertyGen.Order();
                order.CustomerId = cm.Id;
                //order.Customer = cm;
                var orders = db.Orders;
                //var orders = cm.Orders.ToList();

                orders.Add(order);
                db.SaveChanges();
            }

            // assert
        }

        [TestMethod]
        public void CreateCustomer()
        {
            // arrange
            CheckDatabase();
            var newCustomer = propertyGen.Customer();

            // act
            db.Customers.Add(newCustomer);
            db.SaveChanges();

            // assert
            // ??
        }

        public void CheckDatabase()
        {
            var canConnect = db.Database.CanConnect();
            if (!canConnect)
            {
                throw new Exception();
            }
        }

        [TestMethod]
        public void DeleteAllRows<T>(DbSet<T> dbSet) where T: class
        {
            var list = dbSet.ToList();
            dbSet.RemoveRange(list);
            var success = db.SaveChanges();
        }
    }
}