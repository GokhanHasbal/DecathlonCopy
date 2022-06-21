using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Decathlon.Models;

namespace Decathlon.AdminModel
{
    public class ProductModel
    {
        public List<Product> Product { get; set; }
        public List<Category> Category { get; set; }
        public List<Category> SubCategory { get; set; }
        public List<User> User { get; set; }
        public User SingleUser { get; set; }
        public List<Campaing> Campaing { get; set; }
        public List<Brand> Brand { get; set; }
        public Product SingleProduct { get; set; }
        public List<ProductComment> ProductComment { get; set; }
        public ProductComment SingleComment { get; set; }
        public List<ProductImage> ProductImage { get; set; }
    }
}