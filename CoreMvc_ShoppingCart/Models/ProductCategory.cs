using System;
using System.Collections.Generic;

namespace CoreMvc_ShoppingCart.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public DateTime? Firstmodified { get; set; }
        public DateTime? Lastmodified { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
