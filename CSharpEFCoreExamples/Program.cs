using CSharpEFCoreExamples.Examples.Example01;
using CSharpEFCoreExampleTests.GenerateRandomData;

namespace CSharpEFCoreExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Example 01
            new Example01DataGenerator().Run();
            new Example01AInitialSolution().Run();
            new Example01BadSolution01().Run();
            new Example01BadSolution02().Run();
            new Example01BadSolution03().Run();
            new Example01BadSolution04().Run();
            new Example01CorrectSolution().Run();
        }
    }
}