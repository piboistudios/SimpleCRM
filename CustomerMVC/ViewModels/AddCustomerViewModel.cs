using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMVC.ViewModels
{
    public class AddCustomerViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s'-]+$", ErrorMessage = "Letters only!")]
        [Display(Name ="Customer Name")]
        public string name { get; set; }

        [Required]
        [Display(Name ="Credit Limit")]
        public float creditLimit { get; set; }
        [Required]
        [Display(Name = "Date Last Ordered")]
        public DateTime lastOrdered { get; set; }
        [Required]
        [Display(Name = "Date Started")]
        public DateTime dateStarted { get; set; }
        [Required]
        [Display(Name ="Address")]
        public string address { get; set; }
    }
}
