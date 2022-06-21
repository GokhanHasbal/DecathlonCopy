using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decathlon.Models
{
    public class ProductComment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}