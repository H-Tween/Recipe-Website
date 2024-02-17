using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recipe_Website.Models;
using Recipe_Website.Services;

namespace Recipe_Website.Pages
{
    public class RecipeItemModel : PageModel
    {

        private readonly IWebHostEnvironment enviorment;
        private readonly ApplicationDbContext context;

        public string errorMessage = "";
        public string successMessage = "";

        [BindProperty]
        public RecipeDto RecipeDto { get; set; } = new RecipeDto();

        public Recipe Recipe { get; set; } = new Recipe();

        public RecipeItemModel(IWebHostEnvironment enviroment, ApplicationDbContext context)
        {
            this.enviorment = enviroment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            var recipe = context.Recipes.Find(id);

            Recipe = recipe!;
        }
    }
}
