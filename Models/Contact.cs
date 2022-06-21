using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public string Linkedin { get; set; }
        public string Pinterest { get; set; }
        public string GoogleMaps { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
    }
}