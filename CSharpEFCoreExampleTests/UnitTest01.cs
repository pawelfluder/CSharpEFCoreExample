using CSharpEFCoreExample.ContextAddons;
using CSharpEFCoreExample.Data;
using Microsoft.EntityFrameworkCore;

namespace CSharpEFCoreExampleTests
{
    [TestClass]
    public class UnitTest01 : IDisposable
    {
        private readonly DbContextWrapper<OrdersDbContext> wr;
        private readonly RandomPropertyGen propertyGen;

        public UnitTest01()
        {
            wr = new DbContextWrapper<OrdersDbContext>();
            propertyGen = new RandomPropertyGen();
        }

        public void Dispose()
        {
            wr.Db.Dispose();
        }

        [DataRow(5)]
        [TestMethod]
        public void CompletlyNewData(int max)
        {
            DeleteAllRows(wr.Db.Customers);
            DeleteAllRows(wr.Db.Products);
            DeleteAllRows(wr.Db.Orders);

            CreateFewProducts(max);
            CreateFewCustomers(max);
            CreateFewOrders();
        }

        [DataRow(5)]
        [TestMethod]
        public void CreateFewCustomers(int max)
        {
            // arrange
            var customers = wr.Db.Customers.ToList();

            // act
            for (int i = 0; i < max; i++)
            {
                CreateCustomer();
            }

            // assert
            var updatedCustomers = wr.Db.Customers.ToList();
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
            var products = wr.Db.Products;
            products.Add(newProduct);
            wr.Db.SaveChanges();

            // assert
        }

        [TestMethod]
        public void CreateFewOrders()
        {
            // arrange
            var customers = wr.Db.Customers.ToList();

            // act
            foreach (var cm in customers)
            {
                var order = propertyGen.Order();
                order.CustomerId = cm.Id;
                //order.Customer = cm;
                var orders = wr.Db.Orders;
                //var orders = cm.Orders.ToList();

                orders.Add(order);
                wr.Db.SaveChanges();
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
            wr.Db.Customers.Add(newCustomer);
            wr.Db.SaveChanges();

            // assert
            // ??
        }

        public void CheckDatabase()
        {
            var canConnect = wr.Db.Database.CanConnect();
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
            var success = wr.Db.SaveChanges();
        }
    }
}