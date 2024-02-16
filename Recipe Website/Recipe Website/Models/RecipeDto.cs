using System.ComponentModel.DataAnnotations;

namespace Recipe_Website.Models
{
    public class RecipeDto
    {
        [Required, MaxLength(40)]
        public string Title { get; set; } = "";
        public string? Description { get; set; } = "";
        [Required]
        public string Ingredients { get; set; } = "";
        [Required]
        public string Instructions { get; set; } = "";
        [Required, MaxLength(40)]
        public string Category { get; set; } = "";
        public IFormFile? ImageUrl { get; set; }
    }
}
