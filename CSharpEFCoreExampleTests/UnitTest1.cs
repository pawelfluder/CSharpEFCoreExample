using CSharpEFCoreExample;

namespace CSharpEFCoreExampleTests
{
    [TestClass]
    public class UnitTest1 : IDisposable
    {
        private readonly OrdersDbContext db;

        public DateTime[] JoinDates { get; }
        public DateTime[] BithDates { get; }

        public UnitTest1()
        {
            db = new OrdersDbContext();

            JoinDates = new DateTime[]
            {
                DateTime.Parse("2006-03-12"),
                DateTime.Parse("2007-07-24"),
                DateTime.Parse("2008-02-09"),
                DateTime.Parse("2009-04-29"),
                DateTime.Parse("2010-05-01"),
                DateTime.Parse("2011-06-03"),
                DateTime.Parse("2012-04-21"),
            };

            BithDates = new DateTime[]
            {
                DateTime.Parse("1986-03-12"),
                DateTime.Parse("1987-07-24"),
                DateTime.Parse("1988-02-09"),
                DateTime.Parse("1989-04-29"),
                DateTime.Parse("1990-05-01"),
                DateTime.Parse("1991-06-03"),
                DateTime.Parse("1992-04-21"),
            };
        }

        public void Dispose()
        {
            db.Dispose();
        }




        [TestMethod]
        public void CreateCustomer()
        {
            // Assert
            CheckDatabase();
            var customer = CreateNewCustomer();

        }

        private object CreateNewCustomer()
        {
            Random generator = new Random();
            var birthDate = 

            var customer01 = new Customer()
            {
                Name = "Jacek",
                BirthDate = DateTime.Parse("1985-03-13"),
                Id = generator.Next(100000, 1000000),
                CustomerSince = DateTime.Parse("2021-04-29"),
                Orders = new List<Order>
                {
                }
            };
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
        public void DropCustomerTable()
        {
            using (var db = new OrdersDbContext())
            {
                var customers = db.Customers.ToList();
                customers.RemoveAll(x => true);
                var success = db.SaveChanges();
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            var db = new OrdersDbContext();
        db.Dispose();


            using (var db = new OrdersDbContext())
            {
                

                var gg2 = db.Customers.ToList();
                

                db.Customers.Add(customer01);
                var gg4 = db.SaveChanges();
                var gg = db.Customers.ToList();
            }
        }
    }
}