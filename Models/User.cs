using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class User
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email{ get; set; }
        public string Password { get; set; }
        public string TcNo { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public string Code { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int GradeScore { get; set; }
        
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }

        public List<Log> Log { get; set; }
        public List<Address> Address { get; set; }
        public List<ProductComment> ProductComment { get; set; }
        public List<Coupon> Coupon { get; set; }
        public List<Favorite> Favorite { get; set; }
        public List<SiteComment> SiteComment { get; set; }
        public List<Order> Order { get; set; }
        public List<Invoice> Invoice { get; set; }
        public List<Support> Support { get; set; }
        public string Image { get; set; }
        public bool View { get; set; }
    }
}