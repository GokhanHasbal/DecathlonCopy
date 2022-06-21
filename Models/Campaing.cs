using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class Campaing
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public double Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }


    }
}