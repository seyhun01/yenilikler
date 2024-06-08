using System.ComponentModel.DataAnnotations;

namespace OganiShoppingProject.Models.MaintoMain
{
    public class SizeofProduct
    {
        [Key]
        public int Id { get; set; }
        public Product? Product { get; set; }
        public int ProductId { get; set; }
        public Sizes? Sizes { get; set; }
        public int SizesId { get; set; }
    }
}
