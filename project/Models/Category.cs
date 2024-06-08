using System.ComponentModel.DataAnnotations;

namespace OganiShoppingProject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<FavoriteProduct>? FavoriteProducts { get; set; }
        public List<Product>? Product { get; set; }
    }
}
