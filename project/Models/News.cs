﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OganiShoppingProject.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public DateTime Time { get; set; }
        public string? Title { get; set; }
        public string? Uptitle { get; set; }
        public string? Desc { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
