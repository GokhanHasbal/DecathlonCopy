using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class ContactMail
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool View { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
    }
}