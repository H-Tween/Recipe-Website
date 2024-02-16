using System.ComponentModel.DataAnnotations;

namespace Recipe_Website.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        [MaxLength(40)]
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Ingredients { get; set; } = "";
        public string Instructions { get; set; } = "";
        [MaxLength(40)]
        public string Category { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public DateTime CreatedAt { get; set; }

    }
}
