using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMVC.ViewModels
{
    public class AddPropcoViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Display(Name ="Company Name")]
        public string name { get; set; }

        [Required]
        [Display(Name ="Corporate Headquarters")]
        public string address { get; set; }

        [Required]
        [Display(Name="Primary Contact")]
        public string primaryContact { get; set; }


    }
}
