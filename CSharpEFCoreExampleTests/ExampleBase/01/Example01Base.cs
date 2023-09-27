using CSharpEFCoreExample;
using CSharpEFCoreExample.ContextAddons;
using CSharpEFCoreExample.Data;
using CSharpEFCoreExampleTests.ExampleBase.Ef;

namespace CSharpEFCoreExample.ExampleBase
{
    public abstract class Example01Base : EfExampleBase<OrdersDbContext>
    {
        public Example01Base()
            : base(new DbContextWrapper<OrdersDbContext>())
        {}

        protected override void PopulateSqlData()
        {

        }
    }
}
