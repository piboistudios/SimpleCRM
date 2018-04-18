using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMVC.Models
{
    public enum Status
    {
        Pending,
        Completed
    }
    public class Order
    {
        public int ID { get; set; }
        public List<OrderProduct> orderProducts { get; set; }
        
        public DateTime orderDate { get; set; }
        public int customerID { get; set; }
        public Customer customer { get; set; }
        public Status status { get; set; }
        
        

    }
}
