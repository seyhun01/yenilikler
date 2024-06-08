using System.ComponentModel.DataAnnotations;

namespace OganiShoppingProject.Models
{
    public class Settings
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
