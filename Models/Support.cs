using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class Support
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string SupportStatus { get; set; }
        public bool View { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}