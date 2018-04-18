﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMVC.Models
{
    public class OrderProduct
    {
        public int orderProductID { get; set; }

        public int orderID { get; set; }
        

        public int productID { get; set; }
        public int quantity { get; set; }

        // Initialize OrderProducts with a quantity of 1; makes sense
        public OrderProduct()
           
        {
            quantity = 1;
        }

    }
}
