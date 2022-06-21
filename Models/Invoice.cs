using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
        public string InvoiceType { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}