using System.ComponentModel.DataAnnotations;

namespace OganiShoppingProject.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string? Yourmessage { get; set; }
    }
}
