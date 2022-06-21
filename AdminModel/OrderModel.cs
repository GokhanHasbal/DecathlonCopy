using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Decathlon.Models;
namespace Decathlon.AdminModel
{
    public class OrderModel
    {
        public List<Order> Orders { get; set; }
        public Order SingleOrder { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public List<User> Users { get; set; }
        public User SingleUser { get; set; }
        public List<Product> Products { get; set; }

    }
}