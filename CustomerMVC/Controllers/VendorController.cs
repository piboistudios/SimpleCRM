using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerMVC.Data;
using CustomerMVC.Models;
using CustomerMVC.ViewModels;

namespace CustomerMVC.Controllers
{
    public class VendorController : Controller
    {
        private CustomerDbContext context;
        public VendorController(CustomerDbContext context)
        {
            this.context = context;
        }
        
        public IActionResult Index()
        {
            return Redirect("/Vendor/Add");
        }
        public IActionResult Add()
        {
            return View(new AddVendorViewModel());
        }

        [HttpPost]
        public IActionResult Add(AddVendorViewModel addVendorViewModel)
        {
            if (ModelState.IsValid)
            {
                Vendor newVendor = new Vendor
                {
                    name = addVendorViewModel.name,
                    ID = addVendorViewModel.ID,
                    margin = addVendorViewModel.margin
                };
                context.Vendors.Add(newVendor);
                context.SaveChanges();
                return Redirect("/");
            }
            return Redirect("/Customer/AddVendor");

        }
    }
}