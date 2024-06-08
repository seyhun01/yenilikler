using System.ComponentModel.DataAnnotations;

namespace OganiShoppingProject.Models.MaintoMain
{
    public class SizeofSaleoffProduct
    {
        [Key]
        public int Id { get; set; }
        public SaleoffProduct? SaleoffProduct { get; set; }
        public int SaleoffProductId { get; set; }
        public Sizes? Sizes { get; set; }
        public int SizesId { get; set; }
    }
}
