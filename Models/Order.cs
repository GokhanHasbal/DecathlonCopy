using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }

        public string AddressHeader { get; set; }
        public string MyAddress { get; set; }
        public string City { get; set; }
        public string Notes { get; set; }
        public string OrderStatus { get; set; }
        public bool Status { get; set; }
        public bool View { get; set; }
        public bool Delete { get; set; }

    }
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}