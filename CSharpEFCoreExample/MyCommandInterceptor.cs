using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Data.Common;
using System.Linq.Expressions;

namespace CSharpEFCoreExample
{
    public class EfInterceptor : DbCommandInterceptor
    {
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

        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            PrintCommand(command);
            return result;
        }

        public void PrintCommand(DbCommand command)
        {
            var sepStart = "<--------------------";
            var sepStop =   "-------------------->";

            var text = command.CommandText;
            Console.WriteLine(sepStart);
            Console.Write(text);
            Console.WriteLine(string.Empty);
            Console.WriteLine(sepStop);
        }

        public static void Log(Expression<Action> action)
        {
            var methodCallExp = (MethodCallExpression)action.Body;
            string methodName = methodCallExp.Method.Name;

            try
            {
                action.Compile().Invoke();
            }
            catch (Exception ex)
            {

            }

        }

        internal static void PrintLogToConsole()
        {
            // todo
        }

        internal static void PrintLogToPdf()
        {
            // todo
        }
    }
}