using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpEFCoreExamples.ExamplesBase.Example01
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CustomerSince { get; set; }

        public virtual ICollection<Order> Orders { get; set; }


        public override string ToString()
        {
            var text = Name + " (" + Id + ")";
            return text;
        }
    }
}