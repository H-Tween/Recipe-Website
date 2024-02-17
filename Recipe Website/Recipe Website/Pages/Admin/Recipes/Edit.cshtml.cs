using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recipe_Website.Models;
using Recipe_Website.Services;

namespace Recipe_Website.Pages.Admin.Recipes
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment enviorment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public RecipeDto RecipeDto { get; set; } = new RecipeDto();

        public Recipe Recipe { get; set; } = new Recipe();

        public string errorMessage = "";
        public string successMessage = "";

        public EditModel(IWebHostEnvironment enviroment, ApplicationDbContext context)
        {
            this.enviorment = enviroment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            var recipe = context.Recipes.Find(id);

            if (recipe == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            RecipeDto.Title = recipe.Title;
            RecipeDto.Description = recipe.Description;
            RecipeDto.Ingredients = recipe.Ingredients;
            RecipeDto.Instructions = recipe.Instructions;
            RecipeDto.Category = recipe.Category;

            Recipe = recipe;
        }

        public void OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Recipes/index");
                return;
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;
            }

            var recipe = context.Recipes.Find(id);
            if (recipe == null)
            {
                Response.Redirect("/Admin/Recipes/Index");
                return;
            }

            // update the image file if we have a new image file
            string newImageName = recipe.ImageUrl;
            if (RecipeDto.ImageUrl != null)
            {
                newImageName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newImageName += Path.GetExtension(RecipeDto.ImageUrl.FileName);

                string imagePath = enviorment.WebRootPath + "/RecipeImages/" + newImageName;
                using (var stream = System.IO.File.Create(imagePath))
                {
                    RecipeDto.ImageUrl.CopyTo(stream);
                }

                // delete old image
                string oldImagePath = enviorment.WebRootPath + "/RecipeImages/" + recipe.ImageUrl;
                System.IO.File.Delete(oldImagePath);
            }

            // update the product in the database
            recipe.Title = RecipeDto.Title;
            recipe.Description = RecipeDto.Description ?? "";
            recipe.Ingredients = RecipeDto.Ingredients;
            recipe.Instructions = RecipeDto.Instructions;
            recipe.Category = RecipeDto.Category;
            recipe.ImageUrl = newImageName;

            context.SaveChanges();

            Recipe = recipe;

            successMessage = "Recipe updated sucessfully";

            Response.Redirect("/Admin/Recipes/Index");
        }
    }
}
