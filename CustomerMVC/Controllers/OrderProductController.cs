using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerMVC.Models;
using CustomerMVC.ViewModels;
using CustomerMVC.Data;

namespace CustomerMVC.Controllers
{
    public class OrderProductController : Controller
    {
        private CustomerDbContext context;

        // Get the db context
        public OrderProductController(CustomerDbContext context)
        {
            this.context = context;
        }
        
        // Add a items to an order
        public IActionResult Add(int orderID)
        {
            ViewBag.orderID = orderID;
            return View(new AddOrderProductViewModel(context));
        }

        // Once a view model is posted back to this route:
        [HttpPost]
        public IActionResult Add(AddOrderProductViewModel addOrderProductViewModel)
        {
            // Get the order from the db context
            Order order = context.Orders.SingleOrDefault(o => o.ID == addOrderProductViewModel.ID);

            // Let ASP.NET validate the user input and make sure the order isn't invoiced
            if (ModelState.IsValid && order.status != Status.Completed)
            {
                // Create a new order product model to be added to the db
                OrderProduct orderProduct = context.OrderProducts.SingleOrDefault(oP => oP.orderID == addOrderProductViewModel.orderID
                    && oP.productID == addOrderProductViewModel.productID);

                // If an OrderProduct with this Order-Product key doesn't exist, create a key
                if (orderProduct == null)
                {
                    orderProduct = new OrderProduct
                    {
                        productID = addOrderProductViewModel.productID,
                        orderID = order.ID
                    };
                    context.OrderProducts.Add(orderProduct);
                }
                // If it does exist, add to the quantity of the existing OrderProduct record
                else
                {
                    
                    orderProduct.quantity += Math.Max(addOrderProductViewModel.quantity, 1 );
                }
                // Save changes
                context.SaveChanges();

            }
            // Lets see our order
            return Redirect("/Order/Detail?orderID=" + addOrderProductViewModel.orderID.ToString());
        }

    }
}