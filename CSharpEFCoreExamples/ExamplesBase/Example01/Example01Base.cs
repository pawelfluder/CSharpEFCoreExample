using CSharpEFCoreExample.ContextAddons;
using CSharpEFCoreExampleTests.ExampleBase.Ef;
using CSharpEFCoreExampleTests.GenerateRandomData;

namespace CSharpEFCoreExamples.ExamplesBase.Example01
{
    public abstract class Example01Base : EfExampleBase<OrdersDbContext>
    {
        public Example01Base()
            : base(new DbContextWrapper<OrdersDbContext>())
        { }

        protected override void PopulateSqlData()
        {
            
        }
    }
}
