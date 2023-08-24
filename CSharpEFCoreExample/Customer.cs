namespace CSharpEFCoreExample
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CustomerSince { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}