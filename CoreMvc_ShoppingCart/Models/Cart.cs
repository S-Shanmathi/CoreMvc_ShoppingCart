using System;
using System.Collections.Generic;

namespace CoreMvc_ShoppingCart.Models
{
    public partial class Cart
    {
        public int CardId { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ProductCount { get; set; }
        public decimal? ProductTotalprice { get; set; }
        
        //public double amount { get; set; }
        public string? UserName { get; set; }

        public virtual Product? Product { get; set; }
    }
}
