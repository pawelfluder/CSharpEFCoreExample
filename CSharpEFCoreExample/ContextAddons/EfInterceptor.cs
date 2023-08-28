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
            statistics.LogLines.Add("Sql command:");
            statistics.LogLines.Add(text);
            sqlCommandsCount++;
        }

        public void LogMethod(Expression<Action> action)
        {
            // clear
            sqlCommandsCount = 0;

            // arrange
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
                statistics.LogLines.Add("Exception!:");
                statistics.LogLines.Add(ex.Message);
            }

            statistics.LogLines.Add("Sql commands count: " + sqlCommandsCount);
            
            if (textLines.Count == 1)
            {
                statistics.LogLines.Add("Text log: " + textLines.First());
            }
            if (textLines.Count > 1)
            {
                statistics.LogLines.Add("Text log: ");
                statistics.LogLines.AddRange(textLines);
            }
            statistics.LogLines.Add("Method stop: " + methodName);
            statistics.LogLines.Add(statistics.SepStop);

            // clear
            sqlCommandsCount = 0;
            textLines.Clear();
        }

        internal void PrintLogsToConsole()
        {
            statistics.LogLines.ForEach(x => Console.WriteLine(x));
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