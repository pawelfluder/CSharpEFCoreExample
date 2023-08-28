using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEFCoreExample.ContextAddons
{
    public class DbContextWrapper<T> where T: class, IDisposable, new()
    {
        private readonly EfInterceptor efInterceptor;
        public T Db { get; private set; }

        public DbContextWrapper() 
        {
            efInterceptor = new EfInterceptor();
            NewDbContext();
        }

        private void NewDbContext()
        {
            if (Db != null) { Db.Dispose(); }
            var args = new object[] { efInterceptor };
            Db = (T)Activator.CreateInstance(typeof(T), args);
        }

        public void LogMethod(Expression<Action> action)
        {
            NewDbContext();
            efInterceptor.LogMethod(action);
        }

        public void LogText(string text)
        {
            efInterceptor.LogText(text);
        }

        public void PrintLogsToConsole()
        {
            efInterceptor.PrintLogsToConsole();
        }
    }
}
