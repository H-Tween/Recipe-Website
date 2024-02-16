using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recipe_Website.Services;
using System.Net.Http.Headers;
using Recipe_Website.Models;

namespace Recipe_Website.Pages.Admin.Recipes
{
    public class IndexModel : PageModel
    {
        public readonly ApplicationDbContext context;

        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void OnGet()
        {
            Recipes = context.Recipes.OrderByDescending(p => p.Id).ToList();

        }
    }
}
