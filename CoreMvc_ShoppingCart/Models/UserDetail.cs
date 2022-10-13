using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace CoreMvc_ShoppingCart.Models
{
    public partial class UserDetail
    {
        public string UserId { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? PhoneNo { get; set; }
    }
}
