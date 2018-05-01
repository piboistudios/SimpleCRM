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

        // EFC will pass in a DbContext so that we have a context to access the database within the controller
        public CustomerController(CustomerDbContext context)
        {
            this.context = context;
        }

        // Go to add customer view, pass in a view model
        public IActionResult Add()
        {
            AddCustomerViewModel addCustomerViewModel = new AddCustomerViewModel(context);
            return View(addCustomerViewModel);
        }

        // Route when customer view model is posted back.
        [HttpPost]
        public IActionResult Add(AddCustomerViewModel addCustomerViewModel)
        {
            // ASP.NET Core Form validation stuffs
            if(ModelState.IsValid)
            {
                // Copy the view model into a new customer model 
                Customer newCustomer = new Customer
                {
                    ID = addCustomerViewModel.ID,
                    name = addCustomerViewModel.name,
                    creditLimit = addCustomerViewModel.creditLimit,
                    lastOrdered = addCustomerViewModel.lastOrdered,
                    dateStarted = addCustomerViewModel.dateStarted,
                    address = addCustomerViewModel.address
                    
                };
                System.IO.File.WriteAllText("outtttttttttttttttt.txt", newCustomer.ID.ToString() + " , " + addCustomerViewModel.ID.ToString());
                context.Customers.Add(newCustomer);
                context.SaveChanges();
                if(addCustomerViewModel.propCoID != -1)
                {
                    PropcoCustomer newPropcoCustomer = new PropcoCustomer
                    {
                        customerID = newCustomer.ID,
                        propcoID = addCustomerViewModel.propCoID
                    };
                    context.propcoCustomers.Add(newPropcoCustomer);
                    
                }
                context.SaveChanges();
                // Add the customer model to the database as a record
                // Guess what it does
                return Redirect("/");

            }
            // If the model state is invalid redirect to the add customer page; try again
            return View();
        }

        // Remove customer
        public IActionResult Remove()
        {
            ViewBag.title = "Remove Customer";
            ViewBag.customers = context.Customers.ToList();
            return View();
        }

        // Receive a list of customer ids and remove them from the database; save changes afterwards for obvious efficiency reasons
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
