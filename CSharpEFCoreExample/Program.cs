using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CSharpEFCoreExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var source = new ConfigurationSource().
            //var configuration = new ConfigurationBuilder().Add
            //    //.AddJsonFile("appSettings.json");
            ////Configuration = builder.Build();
            ///
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var options = new DbContextOptionsBuilder().Options;
            using (var db = new OrdersDbContext())
            {
                var isCreated = db.Database.EnsureCreated();
                var bool2 = db.Database.CanConnect();
                var bool3 = db.Database.IsSqlite();
                var gg1 = db.Products.ToList();
                var gg2 = db.Customers.ToList();
                var gg3 = db.Orders.ToList();

                
            }
        }
    }
}