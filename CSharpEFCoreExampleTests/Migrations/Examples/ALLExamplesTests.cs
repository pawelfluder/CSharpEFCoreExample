namespace CSharpEFCoreExampleTests.Examples
{
    [TestClass]
    public class ALLExamplesTests
    {
        [TestMethod]
        public void Main()
        {
            // 01
            new Example01AInitialSolution().Run();
            new Example01BadSolution01().Run();
            new Example01BadSolution02().Run();
            new Example01BadSolution03().Run();
            new Example01BadSolution04().Run();
            new Example01CorrectSolution().Run();
        }
    }
}
