using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SilseShop.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SilseShop.Models
{
    public class ShopCart
    {
        public string Id { get; set; }
        public DateTime DateCreation { get; set; }
        public ICollection<ShopCartItem> ShopCartItems { get; set; }
    }
}
