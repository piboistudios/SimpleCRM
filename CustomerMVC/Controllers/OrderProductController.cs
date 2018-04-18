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
        public OrderProductController(CustomerDbContext context)
        {
            this.context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(int orderID)
        {
            ViewBag.orderID = orderID;
            return View(new AddOrderProductViewModel(context));
        }
        [HttpPost]
        public IActionResult Add(AddOrderProductViewModel addOrderProductViewModel)
        {
            Order order = context.Orders.SingleOrDefault(o => o.ID == addOrderProductViewModel.ID);
            if (ModelState.IsValid && order.status != Status.Completed)
            {

                OrderProduct newOrderProduct = context.OrderProducts.SingleOrDefault(oP => oP.orderID == addOrderProductViewModel.orderID
                    && oP.productID == addOrderProductViewModel.productID);
                if (newOrderProduct == null)
                {
                    newOrderProduct = new OrderProduct
                    {
                        productID = addOrderProductViewModel.productID,
                        orderID = order.ID
                    };
                    context.OrderProducts.Add(newOrderProduct);
                }
                else
                {

                    newOrderProduct.quantity += Math.Max(addOrderProductViewModel.quantity, 1 );
                }

                context.SaveChanges();

            }
            return Redirect("/Order/Detail?orderID=" + addOrderProductViewModel.orderID.ToString());
        }

    }
}