using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class Page
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string LitleDescription { get; set; }
        public string Description { get; set; }
        public string SeoDescription{ get; set; }
        public string SeoTitle{ get; set; }
        public string SeoUrl{ get; set; }
        public string SeoKeywords{ get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
    }
}