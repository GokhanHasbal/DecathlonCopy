using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Decathlon.Models;

namespace Decathlon.Models
{
    public class Cart
    {
        private List<CartRow> _cartrows = new List<CartRow>();

        public List<CartRow> CartRows
        {
            get { return _cartrows; }
        }

        public void AddProduct(Product product, int Quantity)
        {
            var rows = _cartrows.FirstOrDefault(x => x.Product.Id == product.Id);

            if (rows == null)
            {
                _cartrows.Add(new CartRow() { Product = product, Quantity = Quantity });
            }
            else
            {
                rows.Quantity += Quantity;
            }
        }
        public void DeleteProduct(Product product)
        {
            _cartrows.RemoveAll(x=>x.Product.Id==product.Id);
        }
        public void Clear()
        {
            _cartrows.Clear();
        }
        public double Total()
        {
            return _cartrows.Sum(x => x.Product.Price * x.Quantity);
        }
    }
    public class CartRow
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}