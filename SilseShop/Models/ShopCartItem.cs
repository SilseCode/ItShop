namespace SilseShop.Models
{
    public class ShopCartItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public Product Product { get; set; }
        public string ShopCartId { get; set; }
        public ShopCart ShopCart { get; set; }
    }
}
