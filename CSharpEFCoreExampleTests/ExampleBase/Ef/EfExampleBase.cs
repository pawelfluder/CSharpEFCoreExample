using CSharpEFCoreExample.ContextAddons;

namespace CSharpEFCoreExampleTests.ExampleBase.Ef
{
    public abstract class EfExampleBase<T> : IExample
    {
        protected readonly IDbContextWrapper<T> wr;
        protected readonly T db;

        protected abstract void Main();

        protected abstract void PopulateSqlData();

        public EfExampleBase(IDbContextWrapper<T> wr)
        {
            this.wr = wr;
            db = wr.Db;
        }

        public void Run()
        {
            PopulateSqlData();
            Main();
            wr.PrintLogsToConsole();
        }
    }
}
