using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CustomerMVC.Models;
using CustomerMVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerMVC.ViewModels
{
    public class AddOrderProductViewModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Order")]
        public int orderID { get; set; }

        [Required]
        [Display(Name ="Product")]
        public int productID { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int quantity { get; set; }

        static List<Product> Products;
        public AddOrderProductViewModel()
        {
            
        }
        public AddOrderProductViewModel(CustomerDbContext context)
        {
            Products = context.Products.ToList();
        }

        static public List<SelectListItem> getProductSelectList()
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            foreach(Product product in Products)
            {
                returnList.Add(new SelectListItem { Value = product.ID.ToString(), Text = product.name });
            }
            return returnList;
        }
        

        
    }
}
