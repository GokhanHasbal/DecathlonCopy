using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class SiteComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public bool View { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}