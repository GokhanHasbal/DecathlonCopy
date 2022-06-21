using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class Coupon
    {
        public int ID { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}