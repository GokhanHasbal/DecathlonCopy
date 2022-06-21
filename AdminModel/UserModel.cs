using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Decathlon.Models;

namespace Decathlon.AdminModel
{
    public class UserModel
    {
        public List<User> User { get; set; }
        public List<Permission> Permission { get; set; }
        public List<Grade> Grade { get; set; }
        public User SingleUser { get; set; }
        
        
    }
}