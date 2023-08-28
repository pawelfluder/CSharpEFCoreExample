using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Linq.Expressions;

namespace CSharpEFCoreExample
{
    public class EfInterceptor : DbCommandInterceptor
    {
        private static Statistics statistics = new Statistics();

        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            LogSqlCommand(command);
            return result;
        }

        public void LogSqlCommand(DbCommand command)
        {
            var text = command.CommandText;
            statistics.LogLines.Add(text);
        }

        public void LogMethod(Expression<Action> action)
        {
            var methodCallExp = (MethodCallExpression)action.Body;
            string methodName = methodCallExp.Method.Name;

            statistics.LogLines.Add(statistics.SepStart);
            statistics.LogLines.Add("Method start: " + methodName);

            try
            {
                action.Compile().Invoke();
            }
            catch (Exception ex)
            {
                statistics.LogLines.Add("Exception!");
                statistics.LogLines.Add(ex.Message);
            }

            statistics.LogLines.Add("Method stop");
            statistics.LogLines.Add(statistics.SepStop);
        }

        internal void PrintLogsToConsole()
        {
            statistics.LogLines.ForEach(x => Console.WriteLine(x));
        }

        internal void PrintLogsToPdf()
        {
            // todo
        }
    }

    //public IMyStatistics Statistics { get; }

    //public MyCommandInterceptor(IMyStatistics statistics)
    //{
    //    Statistics = statistics ?? throw new ArgumentNullException(nameof(statistics));
    //}

    //public override DbCommand CommandCreated(
    //    CommandEndEventData eventData,
    //    DbCommand result)
    //{
    //    var gg = eventData.Command.CommandText;
    //    var gg2 = eventData.Command.Parameters;
    //    return base.CommandCreated(eventData, result);
    //}
}