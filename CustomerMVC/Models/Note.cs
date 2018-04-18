using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMVC.Models
{
    public class Note
    {
        public int ID { get; set; }
        public string content { get; set; }
        public int customerID { get; set; }
        public Customer customer { get; set; }
    }
}
