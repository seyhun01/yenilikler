using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OganiShoppingProject.Models.MaintoMain;

namespace OganiShoppingProject.Models
{
    public class FavoriteProduct
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public string? Name { get; set; }
        public string? InStock { get; set; }
        public string? Decs { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public List<SizeofFavoriteProduct>? SizeofFavoriteProducts { get; set; }
    }
}
