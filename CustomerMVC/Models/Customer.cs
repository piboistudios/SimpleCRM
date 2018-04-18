using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CustomerMVC.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string name { get; set; }
        public DateTime lastOrdered { get; set; }
        public DateTime dateStarted { get; set; }
        public string address { get; set; }
        public double creditLimit { get; set; }
        public double currentBalance { get; set; }
        public List<Order> orders { get; set; }
        public List<Note> notes { get; set; }

        // How broke are they?
        public bool canAfford(double amount)
        {
            return (creditLimit - currentBalance) >= amount;
        }

    }
}
