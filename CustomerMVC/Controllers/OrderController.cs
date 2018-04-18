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
    public class Calculations
    {
        static public double getOrderTotal(CustomerDbContext context, int orderId)
        {
            double total = 0.0;
            Order order = context.Orders.Single(o => o.ID == orderId);
            foreach(OrderProduct orderProduct in from oP in context.OrderProducts.ToList()
                                                 where oP.orderID == orderId
                                                 select oP)
            {
                Product product = context.Products.Single(p => p.ID == orderProduct.productID);
                Vendor vendor = context.Vendors.Single(v => v.ID == product.vendorID);
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
        
        public IActionResult Index()
        {
            return View();
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
        public IActionResult Detail(int orderID = -1)
        {
            if (orderID != -1) ViewBag.orderID = orderID;
            ViewBag.context = context;
            return View();
        }
        public IActionResult Submit(int orderID = 0)
        {
            if (orderID == 0) return View();
            Order order = context.Orders.Single(o => o.ID == orderID);
            Customer customer = context.Customers.Single(c => c.ID == order.customerID);
            if (customer.canAfford(Calculations.getOrderTotal(context, orderID)))
            {
                order.status = Status.Completed;
            }
            else
            {
                ViewBag.OrderSubmissionError = OrderSubmissionErrorStatus.InsufficientFunds;
                return View();
            }
            return Redirect("/Order/Detail?orderID=" + orderID.ToString());
        }


    }

}