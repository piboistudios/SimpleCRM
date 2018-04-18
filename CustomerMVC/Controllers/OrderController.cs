using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerMVC.Data;
using CustomerMVC.ViewModels;
using CustomerMVC.Models;



namespace CustomerMVC.Controllers
{
    public enum OrderSubmissionErrorStatus
    {
        InsufficientFunds
    }
    // Class for abstracting relational calculations, you can see why below
    public class RelationalCalculations
    {
        static public double getOrderTotal(CustomerDbContext context, int orderId)
        {
            double total = 0.0;
            // I'm still not sure why I'm unable to cast a LINQ expression into a List
            Order order = context.Orders.Single(o => o.ID == orderId);
            // For each orderProduct in the OrderProducts of the database that matches this order
            foreach(OrderProduct orderProduct in from oP in context.OrderProducts.ToList()
                                                 where oP.orderID == orderId
                                                 select oP)
            {
                // Get the product based on the product id
                Product product = context.Products.Single(p => p.ID == orderProduct.productID);
                // Get the vendor so you can access his product margin with the company
                Vendor vendor = context.Vendors.Single(v => v.ID == product.vendorID);
                // Total = product base price x product quantity + vendor margin percentage
                total += (product.basePrice * orderProduct.quantity) * (1 + vendor.margin);
            }
            return total;
        }
    }
    public class OrderController : Controller
    {
        
        private CustomerDbContext context;

        
        public OrderController(CustomerDbContext context)
        {
            this.context = context;
        }
        
       
        public IActionResult Add()
        {
            return View(new AddOrderViewModel(context));
        }

        [HttpPost]
        public IActionResult Add(AddOrderViewModel addOrderViewModel)
        {
            if (ModelState.IsValid)
            {
                Order newOrder = new Order
                {
                    ID = addOrderViewModel.ID,
                    customerID = addOrderViewModel.customerID,
                    orderDate = addOrderViewModel.orderDate,

                };
                context.Orders.Add(newOrder);
                context.SaveChanges();
                return Redirect("/Order/Detail?orderID=" + newOrder.ID.ToString());
            }
            return Add();
        }

        // For viewing/modifying orders, pass an orderID into the View via the ViewBag
        // DO NOT PUT NON EXISTENT ORDER IDS INTO VIEW BAG
        public IActionResult Detail(int orderID = -1)
        {
            // If the order exists, pass the orderID and dbContext into the ViewBag to be rendered
            if (context.Orders.SingleOrDefault(o => o.ID == orderID) != null)
            {
                ViewBag.orderID = orderID;
                ViewBag.context = context;
            }
            return View();
        }
        // For submitting orders, this should be redirected from; the fallback is an error message
        public IActionResult Submit(int orderID = 0)
        {
            // If it's a bad order number, go back to order look up
            if (orderID == 0) return Redirect("/Order/Detail");
            // Get the order from the db
            Order order = context.Orders.Single(o => o.ID == orderID);
            // Get the customer from the db based on the order's customerID
            Customer customer = context.Customers.Single(c => c.ID == order.customerID);
            // Make sure our customer isn't broke
            if (customer.canAfford(RelationalCalculations.getOrderTotal(context, orderID)))
            {
                // This order's status is complete, yo
                order.status = Status.Completed;
            }
            else
            {
                // Pass a submission error into the viewbag and display the view
                ViewBag.OrderSubmissionError = OrderSubmissionErrorStatus.InsufficientFunds;
                return View();
            }
            context.SaveChanges();
            // If everything's all good, lets see the order
            return Redirect("/Order/Detail?orderID=" + orderID.ToString());
        }


    }

}