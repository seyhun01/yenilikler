using System.ComponentModel.DataAnnotations;

namespace OganiShoppingProject.Models.MaintoMain
{
    public class SizeofFavoriteProduct
    {
        [Key]
        public int Id { get; set; }
        public FavoriteProduct? FavoriteProduct { get; set; }
        public int FavoriteProductId { get; set; }
        public Sizes? Sizes { get; set; }
        public int SizesId { get; set; }
    }
}
