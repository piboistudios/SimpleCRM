using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CustomerMVC.Data;
using CustomerMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerMVC.ViewModels
{
    public class AddProductViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Display(Name ="Vendor")]
        public int vendorID { get; set; }
        public Vendor vendor { get; set; }
        [Display(Name ="Product Name")]
        public string name { get; set; }
        [Display(Name = "Product Description")]
        public string desc { get; set; }
        [Display(Name = "Base Price")]
        public double basePrice { get; set; }

        // List of all Vendors
        private List<Vendor> Vendors { get; set; }

        // Default constructor for Entity Framework Core
        public AddProductViewModel()
        {

        }

        // Constructor with db context to initialize Vendor list
        public AddProductViewModel(CustomerDbContext context)
        {
            Vendors = context.Vendors.ToList();
        }

        // Return list of Vendors as list of select list items

        public List<SelectListItem> getVendorSelectList()
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            foreach(Vendor vendor in Vendors)
            {
                returnList.Add(new SelectListItem { Text = vendor.name, Value = vendor.ID.ToString() });
            }
            return returnList;
        }
    }
}
