using System.ComponentModel.DataAnnotations.Schema;

namespace OganiShoppingProject.ViewModel.AdminPanel.SaleoffProductVM
{
    public class CreateSaleoffProductVM
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public int LessPercent { get; set; }
        public string? Name { get; set; }
        public int BeforePrice { get; set; }
        public string? Desc { get; set; }
        public string? InStock { get; set; }
        public int AfterPrice { get; set; }
        public List<int>? SizeIds { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
