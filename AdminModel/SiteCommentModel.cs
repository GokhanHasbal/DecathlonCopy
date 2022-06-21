using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Decathlon.Models;
namespace Decathlon.AdminModel
{
    public class SiteCommentModel
    {
        public List<User> User { get; set; }
        public List<SiteComment> SiteComment { get; set; }
        public SiteComment SingleSiteComment { get; set; }
    }
}