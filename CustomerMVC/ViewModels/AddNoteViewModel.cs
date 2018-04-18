using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CustomerMVC.Models;

namespace CustomerMVC.ViewModels
{
    public class AddNoteViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [Display(Name ="Content")]
        public string content { get; set; }
        
        [Display(Name ="Customer")]
        [Required]
        public int customerID { get; set; }
        public List<SelectListItem> Customers { get; set; }
        public AddNoteViewModel()
        {

        }
        public AddNoteViewModel(IEnumerable<Customer> customers)
        {
            Customers = new List<SelectListItem>();
            foreach(Customer customer in customers)
            {
                Customers.Add(new SelectListItem { Text = customer.name, Value = customer.ID.ToString() });
            }
        }
    }
}
