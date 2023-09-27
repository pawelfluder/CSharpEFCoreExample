using CSharpEFCoreExample.ContextAddons;
using CSharpEFCoreExample.Repetition;
using Microsoft.EntityFrameworkCore;

namespace CSharpEFCoreExample.Data
{
    public class OrdersDbContext : DbContext
    {
        private readonly FileService fileService;
        private readonly EfInterceptor efInterceptor;
        private string _connectionString = "Data Source=TemporaryName.db";
        //private static OrdersDbContext _databaseContext = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(efInterceptor);
            optionsBuilder.UseSqlite(_connectionString);
        }

        public string ConnectionString => _connectionString;
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        public OrdersDbContext(EfInterceptor efInterceptor)
        {
            this.efInterceptor = efInterceptor;
            fileService = new FileService();
            _connectionString = new ConnectionString(fileService).Get();
        }

        public OrdersDbContext()
        {
            efInterceptor = new EfInterceptor();
            fileService = new FileService();
            _connectionString = new ConnectionString(fileService).Get();
        }

        //private string GetConnectionString()
        //{
        //    var projectName = "CSharpEFCoreExample";
        //    var startupProjectFolder = fileService.Path.GetProjectFolderPath(projectName);
        //    var upFolder = fileService.Path.MoveDirectoriesUp(startupProjectFolder, 1);
        //    var dbFolder = upFolder + "/" + "Database";
        //    Directory.CreateDirectory(dbFolder);
        //    var dbFileName = "MyCoolDataBase.db";
        //    var dbFilePath = dbFolder + "/" + dbFileName;
        //    dbFilePath = dbFilePath.Replace('\\', '/');
        //    var connectionString = "Data Source=" + dbFilePath;
        //    return connectionString;
        //}
    }
}