using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerMVC.ViewModels;
using CustomerMVC.Models;
using CustomerMVC.Data;

namespace CustomerMVC.Controllers
{
    public class ProductController : Controller
    {
        private CustomerDbContext context;
        public ProductController(CustomerDbContext context)
        {
            this.context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View(new AddProductViewModel(context));
        }

        [HttpPost]
        public IActionResult Add(AddProductViewModel addProductViewModel)
        {
            if (ModelState.IsValid)
            {
                Product newProduct = new Product
                {
                    ID = addProductViewModel.ID,
                    name = addProductViewModel.name,
                    vendorID = addProductViewModel.vendorID,
                    basePrice = addProductViewModel.basePrice,
                    desc = addProductViewModel.desc
                };
                context.Products.Add(newProduct);
                context.SaveChanges();
                return Redirect("/");
            }
            return Redirect("/Customer/AddProduct");
        }

    }
}