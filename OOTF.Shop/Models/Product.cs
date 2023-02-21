using System.ComponentModel.DataAnnotations;

namespace OOTF.Shopping.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Shop Shops { get; set; }
        public int ShopId { get; set; }
    }
}
