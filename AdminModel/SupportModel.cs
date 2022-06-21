using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Decathlon.Models;
namespace Decathlon.AdminModel
{
    public class SupportModel
    {
        public List<User> User { get; set; }
        public List<Support> Support { get; set; }
        public Support SingleSupport { get; set; }
    }
}