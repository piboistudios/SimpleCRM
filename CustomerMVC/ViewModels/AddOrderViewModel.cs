using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CustomerMVC.Data;
using CustomerMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerMVC.ViewModels
{
    public class AddOrderViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; set; }
        
        [Display(Name ="Items")]
        public List<int> orderProductIDs { get; set; }

        public List<OrderProduct> orderProducts { get; set; }

        [Display(Name = "Order Date")]
        [Required]
        public DateTime orderDate { get; set; }

        [Required]
        [Display(Name ="Customer")]
        public int customerID { get; set; }

        // Customer purchasing order
        public Customer customer { get; set; }

        // List of all products
        private List<Product> Products { get; set; }

        // List of all customers
        private List<Customer> Customers { get; set; }
        // Default constructor to avoid EFC errors
        public AddOrderViewModel()
        {
            orderProductIDs = new List<int>();
        }

        // Constructor for passing view model to GET route and initializing product list.
        public AddOrderViewModel(CustomerDbContext context)
        {
            orderProductIDs = new List<int>();
            Products = context.Products.ToList();
            Customers = context.Customers.ToList();
        }

        // Convert product list into select list items where each select list item's text is the products name and the value is the product's ID.
        public List<SelectListItem> getProductSelectList()
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            foreach(Product product in Products)
            {
                returnList.Add(new SelectListItem { Value = product.ID.ToString(), Text = product.name });
            }
            return returnList;
        }

        public List<SelectListItem> getCustomerSelectList()
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            foreach (Customer customer in Customers)
            {
                returnList.Add(new SelectListItem { Value = customer.ID.ToString(), Text = customer.name });
            }
            return returnList;
        }


        // Set the customer of an order given a customer ID.
        public void setCustomer(int customerId, CustomerDbContext context)
        {
            customer = context.Customers.Single(c => c.ID == customerId);
        }


        // Add OrderProducts given a list of product ID numbers.
        public void addOrderProduct(List<int> productIds, CustomerDbContext context)
        {
            foreach(int productId in productIds)
            {
                addOrderProduct(productId, context);
            }
            //use LINQ to select all order products where the orderID is indeed this order
            orderProducts = (List<OrderProduct>)(from oP in context.OrderProducts.ToList()
                            where oP.orderID == ID
                            select oP);

        }

        // Add a single order product given a product ID.
        public void addOrderProduct(int productId, CustomerDbContext context)
        {
            OrderProduct newOrderProduct = new OrderProduct { productID = productId, orderID = ID };
            context.OrderProducts.Add(newOrderProduct);
            
        }

    }
}
