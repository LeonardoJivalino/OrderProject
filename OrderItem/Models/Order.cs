namespace OrderItem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string SalesOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; } // Foreign Key
        public ComCustomer Customer { get; set; } // Navigation Property
    }
}
