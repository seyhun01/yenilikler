using OganiShoppingProject.Models.MaintoMain;
using System.ComponentModel.DataAnnotations;

namespace OganiShoppingProject.Models
{
    public class Sizes
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<SizeofFavoriteProduct>? SizeofFavoriteProducts { get; set; }
        public List<SizeofSaleoffProduct>? SizeofSaleoffProducts { get; set; }

        public List<SizeofProduct>? SizeofProducts { get; set; }
    }
}
