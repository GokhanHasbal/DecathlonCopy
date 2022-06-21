using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.AdminModel
{
    public class Notification
    {
        public DateTime Date { get; set; }
        public string NameSurname { get; set; }
        public string Icon { get; set; }
        public string Content { get; set; }
        public string Color { get; set; }
        public string Link { get; set; }
    }
}