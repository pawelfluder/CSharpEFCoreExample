using CSharpEFCoreExample.ContextAddons;
using CSharpEFCoreExample.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CSharpEFCoreExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tmp = new DbContextWrapper<OrdersDbContext>();
        }
    }
}