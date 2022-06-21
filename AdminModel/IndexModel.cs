using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Decathlon.Models;

namespace Decathlon.AdminModel
{
    public class IndexModel
    {
        public List<User> User { get; set; }
        public int UserTotalCount { get; set; }
        public int UserActiveCount { get; set; }
        public int UserPasiveCount { get; set; }
    }
}