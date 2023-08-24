namespace CSharpEFCoreExample
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}