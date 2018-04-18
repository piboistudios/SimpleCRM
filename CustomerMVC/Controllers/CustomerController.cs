using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerMVC.Models;
using CustomerMVC.ViewModels;
using CustomerMVC.Data;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerMVC.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerDbContext context;

        public CustomerController(CustomerDbContext context)
        {
            this.context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            AddCustomerViewModel addCustomerViewModel = new AddCustomerViewModel();
            return View(addCustomerViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCustomerViewModel addCustomerViewModel)
        {
            if(ModelState.IsValid)
            {
                Customer newCustomer = new Customer
                {
                    ID = addCustomerViewModel.ID,
                    name = addCustomerViewModel.name,
                    creditLimit = addCustomerViewModel.creditLimit,
                    lastOrdered = addCustomerViewModel.lastOrdered,
                    dateStarted = addCustomerViewModel.dateStarted,
                    address = addCustomerViewModel.address
                    
                };
                context.Customers.Add(newCustomer);
                context.SaveChanges();
                return Redirect("/");

            }
            return View();
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Customer";
            ViewBag.customers = context.Customers.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] customerIds)
        {
            foreach(int customerId in customerIds)
            {
                Customer toRemove = context.Customers.Single<Customer>(c => c.ID == customerId);
                context.Customers.Remove(toRemove);
            }
            context.SaveChanges();
            return Redirect("/");
        }
        
    }
}
