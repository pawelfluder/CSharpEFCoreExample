using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

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
            var dbContext = new OrdersDbContext();
            var service = new DiscoutService(dbContext);
            var startDate = DateTime.Now;
            var productCodes = new List<string>
            {
                "123-654-323",
                "972-251-920",
                "145-104-023"
            };

            service.Process(startDate, productCodes);
        }
    }
}