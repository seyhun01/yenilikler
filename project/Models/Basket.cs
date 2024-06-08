using System.ComponentModel.DataAnnotations;

namespace OganiShoppingProject.Models
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }
}
