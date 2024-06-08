using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel.DataAnnotations.Schema;

namespace OganiMasterTEmplate.Models.Blog
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public DateTime  Time { get; set; }
        public string Title { get; set; }
        public string Uptitle { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
