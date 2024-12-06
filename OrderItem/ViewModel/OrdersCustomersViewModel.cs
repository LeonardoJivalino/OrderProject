using OrderItem.Models;

namespace OrderItem.ViewModel
{
    public class OrdersCustomersViewModel
    {
        public int No { get; set; }
        public string OrderNo { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public string CustomerName { get; set; }
    }
}
