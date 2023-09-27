using CSharpEFCoreExample.ContextAddons;
using CSharpEFCoreExample.Data;
using Microsoft.EntityFrameworkCore;

namespace CSharpEFCoreExampleTests.GenerateRandomData
{
    public class Generator : IDisposable
    {
        private readonly DbContextWrapper<OrdersDbContext> wr;
        private OrdersDbContext Db { get; }
        private readonly RandomPropertyGen propertyGen;

        public Generator()
        {
            wr = new DbContextWrapper<OrdersDbContext>();
            Db = wr.Db;
            propertyGen = new RandomPropertyGen();
        }

        public void Generate()
        {
            CompletlyNewData(5);
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public void CompletlyNewData(int max)
        {
            DeleteAllRows(Db.Customers);
            DeleteAllRows(Db.Products);
            DeleteAllRows(Db.Orders);

            CreateFewProducts(max);
            CreateFewCustomers(max);
            CreateFewOrders();
        }

        public void CreateFewCustomers(int max)
        {
            // arrange
            var customers = Db.Customers.ToList();

            // act
            for (int i = 0; i < max; i++)
            {
                CreateCustomer();
            }

            // assert
            var updatedCustomers = Db.Customers.ToList();
            Assert.AreEqual(customers.Count + max, updatedCustomers.Count);
        }

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

        public void CreateProduct()
        {
            // arrange
            var newProduct = propertyGen.Product();

            // act
            var products = Db.Products;
            products.Add(newProduct);
            Db.SaveChanges();

            // assert
        }

        public void CreateFewOrders()
        {
            // arrange
            var customers = Db.Customers.ToList();

            // act
            foreach (var cm in customers)
            {
                var order = propertyGen.Order();
                order.CustomerId = cm.Id;
                //order.Customer = cm;
                var orders = Db.Orders;
                //var orders = cm.Orders.ToList();

                orders.Add(order);
                Db.SaveChanges();
            }

            // assert
        }

        public void CreateCustomer()
        {
            // arrange
            CheckDatabase();
            var newCustomer = propertyGen.Customer();

            // act
            Db.Customers.Add(newCustomer);
            Db.SaveChanges();

            // assert
            // ??
        }

        public void CheckDatabase()
        {
            var canConnect = Db.Database.CanConnect();
            if (!canConnect)
            {
                throw new Exception();
            }
        }

        public void DeleteAllRows<T>(DbSet<T> dbSet) where T : class
        {
            var list = dbSet.ToList();
            dbSet.RemoveRange(list);
            var success = Db.SaveChanges();
        }
    }
}