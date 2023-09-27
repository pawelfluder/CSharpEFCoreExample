using System.Linq.Expressions;

namespace CSharpEFCoreExample.ContextAddons
{
    public interface IDbContextWrapper<T>
    {
        T Db { get; }

        public void LogMethod(Expression<Action> action);
        public void LogText(string text);
        public void PrintLogsToConsole();
    }
}
