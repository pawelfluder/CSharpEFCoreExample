﻿using CSharpEFCoreExample.Repetition;
using Microsoft.EntityFrameworkCore;

namespace CSharpEFCoreExample
{
    public class OrdersDbContext : DbContext
    {
        private readonly FileService fileService;
        private string _connectionString = "Data Source=TemporaryString.db";
        private static OrdersDbContext _databaseContext = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new MyCommandInterceptor());
            optionsBuilder.UseSqlite(_connectionString);
        }

        public string ConnectionString => _connectionString;
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        public OrdersDbContext()
        {
            fileService = new FileService();
            _connectionString = GetConnectionString();
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