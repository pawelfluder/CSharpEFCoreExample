using CSharpEFCoreExample.Repetition;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CSharpEFCoreExample
{
    public class OrdersDbContext : DbContext
    {
        private readonly FileService fileService;
        private readonly EfInterceptor efInterceptor;
        private string _connectionString = "Data Source=TemporaryName.db";
        //private static OrdersDbContext _databaseContext = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new EfInterceptor());
            optionsBuilder.UseSqlite(_connectionString);
        }

        public string ConnectionString => _connectionString;
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        public OrdersDbContext()
        {
            efInterceptor = new EfInterceptor();
            fileService = new FileService();
            _connectionString = GetConnectionString();
        }

        public void LogMethod(Expression<Action> action)
        {
            efInterceptor.LogMethod(action);
        }

        public void PrintLogsToConsole()
        {
            efInterceptor.PrintLogsToConsole();
        }

        private string GetConnectionString()
        {
            var projectName = "CSharpEFCoreExample";
            var startupProjectFolder = fileService.Path.GetProjectFolderPath(projectName);
            var upFolder = fileService.Path.MoveDirectoriesUp(startupProjectFolder, 1);
            var dbFolder = upFolder + "/" + "database";
            Directory.CreateDirectory(dbFolder);
            var dbFileName = "MyCoolDataBase.db";
            var dbFilePath = dbFolder + "/" + dbFileName;
            var connectionString = "Data Source=" + dbFilePath;
            return connectionString;
        }
    }
}