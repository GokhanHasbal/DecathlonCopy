using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class NewsGet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public bool View { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
    }
}