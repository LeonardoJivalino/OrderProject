using OrderItem.Models;

namespace OrderItem.ViewModel
{
    public class AddSalesOrderViewModel
    {
        public string ItemNo { get; set; }
        public string OrderNo { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public string CustomerName { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }

    }
}
