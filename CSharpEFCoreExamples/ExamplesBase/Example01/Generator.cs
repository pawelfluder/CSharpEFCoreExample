using CSharpEFCoreExample.ContextAddons;
using CSharpEFCoreExamples.AA_ExamplesBase.Example01;
using CSharpEFCoreExamples.ExamplesBase.Example01;
using Microsoft.EntityFrameworkCore;

namespace CSharpEFCoreExampleTests.GenerateRandomData
{
    public class Example01DataGenerator
    {
        private DbContextWrapper<OrdersDbContext> wr;
        private OrdersDbContext db;
        private readonly RandomPropertyGen propertyGen;
        private int correctDataCount;

        public Example01DataGenerator()
        {
            propertyGen = new RandomPropertyGen();
            correctDataCount = 5;
        }

        public void Run()
        {
            wr = new DbContextWrapper<OrdersDbContext>();
            db = wr.Db;
            var isDataCorrect = IsDataCorrect();
            if (isDataCorrect)
            {
                db.Dispose();
                return;
            }

            ClearAll();
            AddCompletlyNewData(correctDataCount);
            db.Dispose();
        }

        private bool IsDataCorrect()
        {
            var customers = db.Customers.ToList();
            return customers.Count() == correctDataCount;
        }

        private void ClearAll()
        {
            foreach (var entity in db.Customers)
                db.Customers.Remove(entity);
            db.SaveChanges();
        }

        private void AddCompletlyNewData(int max)
        {
            DeleteAllRows(db.Customers);
            DeleteAllRows(db.Products);
            DeleteAllRows(db.Orders);

            CreateFewProducts(max);
            CreateFewCustomers(max);
            CreateFewOrders();
        }

        private void CreateFewCustomers(int max)
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
        }

        private void CreateFewProducts(int max)
        {
            // arrange
            // act
            for (int i = 0; i < max; i++)
            {
                CreateProduct();
            }
            // assert
        }

        private void CreateProduct()
        {
            // arrange
            var newProduct = propertyGen.Product();

            // act
            var products = db.Products;
            products.Add(newProduct);
            db.SaveChanges();

            // assert
        }

        private void CreateFewOrders()
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

        private void CreateCustomer()
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

        private void CheckDatabase()
        {
            var canConnect = db.Database.CanConnect();
            if (!canConnect)
            {
                throw new Exception();
            }
        }

        private void DeleteAllRows<T>(DbSet<T> dbSet) where T : class
        {
            var list = dbSet.ToList();
            dbSet.RemoveRange(list);
            var success = db.SaveChanges();
        }
    }
}