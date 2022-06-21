using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }

        public List<User> User { get; set; }
    }
}