using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMVC.ViewModels
{
    public class AddVendorViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [Display(Name="Vendor Name")]
        public string name { get; set; }
        
        [Required]
        [Display(Name ="Product Margin")]
        public float margin { get; set; }
    }
}
