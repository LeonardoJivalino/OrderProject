using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderItem.Data;
using OrderItem.Models;
using OrderItem.ViewModel;
using System;

namespace OrderItem.Controllers
{
    public class AddSalesOrderController : Controller
    {
        private readonly OrderContext _context;

        public AddSalesOrderController(OrderContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> AddSalesOrder()
        {
            var customers = await _context.ComCustomers.ToListAsync();
            List<Customer> customerList = new List<Customer>();
            foreach (var cst in customers)
            {
                Customer customer = new Customer();
                customer.Id = cst.ComCustomerId;
                customer.Name = cst.CustomerName;
                customerList.Add(customer);
            }
            return View(customerList);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTableData([FromBody] List<AddSalesOrderViewModel> tableData)
        {
            var customer = await _context.ComCustomers.Where(x => x.CustomerName == tableData[0].CustomerName).SingleOrDefaultAsync();
            if (tableData.Count > 0)
            {
                _context.Database.BeginTransaction();
                SoOrder order = new SoOrder();
                order.OrderNo = tableData[0].OrderNo;
                order.OrderDate = tableData[0].OrderDate;
                order.ComCustomerId = customer.ComCustomerId;
                order.Address = tableData[0].Address;
                await _context.SoOrders.AddAsync(order);
                await _context.SaveChangesAsync();
                foreach (var row in tableData)
                {

                    SoItem item = new SoItem();
                    item.SoOrderId = order.SoOrderId;
                    item.ItemName = row.ItemName;
                    item.Quantity = row.Quantity;
                    item.Price = row.Price;
                    await _context.SoItems.AddAsync(item);
                }
                await _context.SaveChangesAsync();
                _context.Database.CommitTransaction();
                return Json(new { message = "Items saved successfully!" });
                
            }
            else
            {
                return Json(new { message = "No items to save!" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesOrder,OrderDate,Customer")] Order order)
        {
            if (ModelState.IsValid)
            {
                // Add the new order to the DbContext
                _context.Add(order);
                await _context.SaveChangesAsync(); // Save changes to the database

                return RedirectToAction(nameof(Index)); // Redirect after successful creation
            }
            return View(order);
        }
    }
}
