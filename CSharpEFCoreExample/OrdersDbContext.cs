using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CSharpEFCoreExample
{
    public class OrdersDbContext : DbContext
    {
        private const string ConnectionString = "Data Source=MyCoolDataBase.db";
        private static OrdersDbContext _databaseContext = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(ConnectionString);

        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        //public OrdersDbContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //protected readonly IConfiguration Configuration;

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        //}
    }
}