using OganiShoppingProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OganiShoppingProject.ViewModel.AdminPanel.ProductVM
{
    public class CreateProductVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public string? Desc { get; set; }
        public string? InStock { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<int>? SizeIds { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
