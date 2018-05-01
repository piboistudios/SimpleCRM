using CustomerMVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMVC.ViewModels
{
    public class ViewCoViewModel
    {
        public int ID { get; set; }
        public CustomerDbContext context { get; set; }
        public List<SelectListItem> propcos { get; set; }
    }
}
