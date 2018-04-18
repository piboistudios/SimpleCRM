using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerMVC.Data;
using CustomerMVC.ViewModels;
using CustomerMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerMVC.Controllers
{
    public class NoteController : Controller
    {
        private CustomerDbContext context;
        public NoteController(CustomerDbContext context)
        {
            this.context = context;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            return Redirect("/Note/Add");
        }
        public IActionResult Add()
        {
            AddNoteViewModel addNoteViewModel = new AddNoteViewModel(context.Customers.ToList());
            return View(addNoteViewModel);

        }
        [HttpPost]
        public IActionResult Add(AddNoteViewModel addNoteViewModel)
        {
            if (ModelState.IsValid)
            {
                Note newNote = new Note
                {
                    ID = addNoteViewModel.ID,
                    content = addNoteViewModel.content,
                    customerID = addNoteViewModel.customerID
                };
                context.Notes.Add(newNote);
                context.SaveChanges();
            }
            return Redirect("/");
        }

    }
}
