using Microsoft.AspNetCore.Identity;

namespace OOTF.Shopping.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Manager { get; set; } 
        public int ManagerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
