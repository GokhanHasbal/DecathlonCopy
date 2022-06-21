using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Decathlon.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Rate { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public string SeoUrl { get; set; }
        public string SeoTitle { get; set; }
        public double Tax { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? CampaingId { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<ProductImage> ProductImage { get; set; }
        public List<ProductComment> ProductComment { get; set; }
        public List<Favorite> Favorite { get; set; }
    }
}