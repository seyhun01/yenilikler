using System.ComponentModel.DataAnnotations;

namespace OganiShoppingProject.Models
{
    public class Contact
    {
        [Key]
        public int İd { get; set; }
        public string? Address { get; set; }
        public int PhoneNumber { get; set; }
        public string? OpenTime { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? MapAddress { get; set; }
    }
}
