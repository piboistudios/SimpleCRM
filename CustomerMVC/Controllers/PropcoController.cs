using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerMVC.Data;
using CustomerMVC.Models;
using CustomerMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerMVC.Controllers
{
    public class PropcoController : Controller
    {
        CustomerDbContext context;
        List<SelectListItem> propcos;
        
        public PropcoController(CustomerDbContext context)
        {
            this.context = context;
            propcos = new List<SelectListItem>();
            foreach(Propco propco in context.Propcos.ToList())
            {
                propcos.Add(new SelectListItem
                {
                    Value = propco.ID.ToString(),
                    Text = propco.name
                    
                });
            }
        }
        public IActionResult ViewCo(int id=-1)
        {
            return View(new ViewCoViewModel
            {
                ID = id,
                context = context,
                propcos = propcos
            });
        }
        public IActionResult Add()
        {

            return View(new AddPropcoViewModel());
        }
        [HttpPost]
        public IActionResult Add(AddPropcoViewModel addPropcoViewModel)
        {
            if(ModelState.IsValid)
            {
                Propco newPropco = new Propco
                {
                    ID = addPropcoViewModel.ID,
                    name = addPropcoViewModel.name,
                    address = addPropcoViewModel.address,
                    primaryContact = addPropcoViewModel.primaryContact
                };
                context.Propcos.Add(newPropco);
                context.SaveChanges();
                return Redirect("/Propco/ViewCo?id=" + newPropco.ID.ToString());
            }
            else
            {
                return Redirect("/Propco/Add");
            }
        }
        

    }
}