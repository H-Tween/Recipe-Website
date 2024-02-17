using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recipe_Website.Services;

namespace Recipe_Website.Pages.Admin.Recipes
{
    public class DeleteModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;
        public DeleteModel(IWebHostEnvironment environment, ApplicationDbContext context) 
        { 
            this.environment = environment;
            this.context = context; 
        }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Recipes/Index");
                return;
            }

            var recipe = context.Recipes.Find(id);
            if (recipe == null) 
            {
                Response.Redirect("/Admin/Recipes/Index");
                return; 
            }

            string imagePath = environment.WebRootPath + "/RecipeImages/" + recipe.ImageUrl;
            System.IO.File.Delete(imagePath);

            context.Recipes.Remove(recipe);
            context.SaveChanges();

            Response.Redirect("/Admin/Recipes/Index");
        }
    }
}
