using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderItem.Data;
using OrderItem.ViewModel;

namespace OrderItem.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderContext _context;

        public OrderController(OrderContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OrderAction()
        {
            var orders = await _context.SoOrders.ToListAsync();
            int orderNumber = 1;
            List<OrdersCustomersViewModel> ocvmList = new List<OrdersCustomersViewModel>();
            foreach (var order in orders) 
            {
                var customer = await _context.ComCustomers.Where(x => x.ComCustomerId == order.ComCustomerId).SingleOrDefaultAsync();
                OrdersCustomersViewModel ocvm = new OrdersCustomersViewModel();
                ocvm.No = orderNumber;
                ocvm.OrderNo = order.OrderNo;
                ocvm.OrderDate = order.OrderDate;
                ocvm.CustomerName = customer.CustomerName;
                ocvmList.Add(ocvm);
                orderNumber++;
            }
            

            return View(ocvmList);
        }

        public async Task<IActionResult> EditOrder(string orderNo)
        {
            var orders = await _context.SoOrders.ToListAsync();
            int orderNumber = 1;
            List<OrdersCustomersViewModel> ocvmList = new List<OrdersCustomersViewModel>();
            foreach (var order in orders)
            {
                var customer = await _context.ComCustomers.Where(x => x.ComCustomerId == order.ComCustomerId).SingleOrDefaultAsync();
                OrdersCustomersViewModel ocvm = new OrdersCustomersViewModel();
                ocvm.No = orderNumber;
                ocvm.OrderNo = order.OrderNo;
                ocvm.OrderDate = order.OrderDate;
                ocvm.CustomerName = customer.CustomerName;
                ocvmList.Add(ocvm);
                orderNumber++;
            }


            return View(ocvmList);
        }
    }
}
