using System;
using System.Collections.Generic;

namespace CoreMvc_ShoppingCart.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }
        public string? ProductReview { get; set; }
        public int? ProductCount { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? ProductSold { get; set; }

        public virtual ProductCategory? Category { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
