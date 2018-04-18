using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMVC.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int vendorID { get; set; }
        public Vendor vendor { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public double basePrice { get; set; }
    }
}
