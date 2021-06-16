using System.Collections.Generic;

namespace SilseShop.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string ImgUrl { get; set; }
        public string TypeName { get; set; }
        public int Count { get; set; }
        public ProductViewModel(int id, string name, decimal? price, string imgUrl, string typeName)
        {
            Id = id;
            Name = name;
            Price = price;
            ImgUrl = imgUrl;
            TypeName = typeName;
            Count = 1;
        }
        public ProductViewModel(Product product) : this(product.Id, product.Name, product.Price, product.ImgUrl, product.Type?.Type)
        {
            Count = 1;
        }

    }
}
