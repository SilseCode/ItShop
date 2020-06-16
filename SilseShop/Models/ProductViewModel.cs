using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilseShop.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string ImgUrl { get; set; }
        public string TypeName { get; set; }
        public ProductViewModel(string name, decimal? price, string imgUrl, string typeName)
        {
            Name = name;
            Price = price;
            ImgUrl = imgUrl;
            TypeName = typeName;
        }
        
    }
}
