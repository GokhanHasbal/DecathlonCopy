using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string BuildingNo { get; set; }
        public string ApartmentNo { get; set; }
        public string OpenAddress { get; set; }
        public bool Status { get; set; }
        public bool Selection { get; set; }
        public bool Delete { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}