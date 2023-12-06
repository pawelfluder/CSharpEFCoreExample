using CSharpEFCoreExample.ContextAddons;

namespace CSharpEFCoreExamples.ExamplesBase.Example01
{
    public interface IDbContextWrapper01 : IDbContextWrapper<OrdersDbContext> { }
}
