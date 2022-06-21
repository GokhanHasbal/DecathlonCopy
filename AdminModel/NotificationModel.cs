using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Decathlon.Models;

namespace Decathlon.AdminModel
{
    public class NotificationModel
    {
        public int Count { get; set; }

        public List<ProductComment> ProductComment { get; set; }
        public List<SiteComment> SiteComment { get; set; }
        public List<ContactMail> ContactMail { get; set; }
        public List<Support> Support { get; set; }
        public List<NewsGet> NewsGet { get; set; }
        public List<User> User { get; set; }
        public List<User> CommentUser { get; set; }
        public List<Order> Order { get; set; }
        
    }
}