using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Linq.Expressions;

namespace CSharpEFCoreExample.ContextAddons
{
    public class EfInterceptor : DbCommandInterceptor
    {
        private static Statistics statistics = new Statistics();
        private int sqlCommandsCount = 0;
        private List<string> textLines = new List<string>();
        private bool processStarted;

        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            if (processStarted) { LogSqlCommand(command); }
            return result;
        }

        public void LogSqlCommand(DbCommand command)
        {
            sqlCommandsCount++;
            var text = command.CommandText;
            statistics.AddLogLines("Sql command:");
            statistics.AddLogLines(text);
        }

        public void LogMethod(Expression<Action> action)
        {
            // clear
            processStarted = true;

            // arrange
            var methodCallExp = (MethodCallExpression)action.Body;
            string methodName = methodCallExp.Method.Name;
            statistics.AddLogLines(statistics.SepStart);
            statistics.AddLogLines("Method start: " + methodName);

            try
            {
                action.Compile().Invoke();
            }
            catch (Exception ex)
            {
                statistics.AddLogLines("Exception!:");
                statistics.AddLogLines(ex.Message);
            }

            statistics.AddLogLines("Sql commands count: " + sqlCommandsCount);
            
            if (textLines.Count == 1)
            {
                statistics.AddLogLines("Text log: " + textLines.First());
            }
            if (textLines.Count > 1)
            {
                statistics.AddLogLines("Text log: ");
                statistics.AddLogLines(textLines);
            }
            statistics.AddLogLines("Method stop: " + methodName);
            statistics.AddLogLines(statistics.SepStop);

            // clear
            textLines.Clear();
            processStarted = false;
        }

        internal void PrintLogsToConsole()
        {
            statistics.Print();
            sqlCommandsCount = 0;
        }

        internal void PrintLogsToPdf()
        {
            // todo
        }

        internal void LogText(string text)
        {
            textLines.Add(text);
        }
    }
}

//public override DbCommand CommandCreated(
//    CommandEndEventData eventData,
//    DbCommand result)
//{
//    var gg = eventData.Command.CommandText;
//    var gg2 = eventData.Command.Parameters;
//    return base.CommandCreated(eventData, result);
//}