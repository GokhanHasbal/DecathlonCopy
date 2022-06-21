using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}