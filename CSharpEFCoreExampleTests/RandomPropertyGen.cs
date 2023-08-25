using CSharpEFCoreExample;

namespace CSharpEFCoreExampleTests
{
    internal class RandomPropertyGen
    {
        public string Name()
        {
            var names = new string[]
            {
                "Jacek", "Marek", "Adam", "Daniel", "Paweł",
            };

            var index = new Random().Next(names.Length);
            return names[index];
        }

        public Product Product()
        {
            var product = new Product()
            {
                Id = Id(),
                Code = Code(),
                Description = Description(),
            };

            return product;
        }

        private string Description()
        {
            var description = "description";
            return description;
        }

        private string Code()
        {
            var generator = new Random();
            var one = generator.Next(100, 1000);
            var two = generator.Next(100, 1000);
            var three = generator.Next(100, 1000);
            var gg = "-";
            var code = one + gg + two + gg + three;
            return code;
        }

        public DateTime BirthDate()
        {
            var start = DateTime.Parse("1960-01-01");
            var len = 365 * 40;
            var tmp = start.AddDays(len);
            var date = start.AddDays(new Random().Next(len));
            return date;
        }

        public DateTime JoinDate(DateTime birthDate)
        {
            var start = birthDate.AddDays(365 * 18);
            var end = DateTime.Now;
            var len = (end - start).Days;
            var date = start.AddDays(new Random().Next(len));
            return date;
        }

        public DateTime OrderTime()
        {
            var end = DateTime.Now;
            var start = end.AddYears(-3);
            var len = 3 * 365;
            var date = start.AddDays(new Random().Next(len));
            return date;
        }

        public DateTime TimeBeforeInput(DateTime inputTime)
        {
            var end = DateTime.Now;
            var len = (end - inputTime).Days;
            var date = inputTime.AddDays(new Random().Next(len));
            return date;
        }

        public int Id()
        {
            var generator = new Random();
            var id = generator.Next(100000, 1000000);
            return id;
        }

        public string Id2()
        {
            var generator = new Random();
            var id = generator.Next(100000, 1000000);
            var gg = "-";
            var id2 = id.ToString()
                .Insert(1, gg)
                .Insert(3, gg)
                .Insert(5, gg);
            return id2;
        }

        public Customer Customer()
        {
            var birthdate = BirthDate();
            var customer = new Customer()
            {
                Name = Name(),
                BirthDate = birthdate,
                Id = Id(),
                CustomerSince = JoinDate(birthdate),
                Orders = new List<Order>(),
            };

            return customer;
        }

        public Order Order()
        {
            var orderDate = OrderTime();
            var shipDate = TimeBeforeInput(orderDate);
            var deliveryDate = TimeBeforeInput(shipDate);
            var order = new Order()
            {
                Id = Id(),
                Product = Product(),
                OrderDate = orderDate,
                ShipDate = shipDate,
                DeliveryDate = deliveryDate,
            };

            return order;
        }

        public void Method()
        {
            var JoinDates = new DateTime[]
            {
                DateTime.Parse("2006-03-12"),
                DateTime.Parse("2007-07-24"),
                DateTime.Parse("2008-02-09"),
                DateTime.Parse("2009-04-29"),
                DateTime.Parse("2010-05-01"),
                DateTime.Parse("2011-06-03"),
                DateTime.Parse("2012-04-21"),
            };

            var BithDates = new DateTime[]
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

        public void Method2()
        {
            var gg = new List<string>()
            {
                "123-654-323",
                "972-251-920",
                "145-104-023",
            };
            
        }
    }
}
