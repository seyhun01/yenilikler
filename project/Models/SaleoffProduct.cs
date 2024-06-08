using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OganiShoppingProject.Models.MaintoMain;

namespace OganiShoppingProject.Models
{
    public class SaleoffProduct
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public int LessPercent { get; set; }
        public string? Name { get; set; }
        public int BeforePrice { get; set; }
        public string? Desc { get; set; }
        public string? InStock { get; set; }
        public int AfterPrice { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public List<SizeofSaleoffProduct>? SizeofSaleoffProducts { get; set; }
    }
}
