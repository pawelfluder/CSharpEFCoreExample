using System.ComponentModel.DataAnnotations;

namespace CSharpEFCoreExamples.ExamplesBase.Example01
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}