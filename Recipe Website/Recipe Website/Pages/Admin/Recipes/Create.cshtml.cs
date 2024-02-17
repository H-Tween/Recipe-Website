using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recipe_Website.Models;
using Recipe_Website.Services;
using System.Net.Http.Headers;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace Recipe_Website.Pages.Admin.Recipes
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment enviorment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public RecipeDto RecipeDto { get; set; } = new RecipeDto();

        public CreateModel(IWebHostEnvironment enviroment, ApplicationDbContext context)
        {
            this.enviorment = enviroment;
            this.context = context; 
        }
        public void OnGet()
        {
        }

        public string errorMessage = "";
        public string successMessage = "";

        public void OnPost()
        {
            if (RecipeDto.ImageUrl == null)
            {
                ModelState.AddModelError("RecipeDto.ImageUrl", "The image file is required");
            }
            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all of the required fields";
                return;
            }

            // save image file
            string newImageName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newImageName += Path.GetExtension(RecipeDto.ImageUrl!.FileName);

            string ImagePath = enviorment.WebRootPath + "/RecipeImages/" + newImageName;
            using (var stream = System.IO.File.Create(ImagePath))
            {
                RecipeDto.ImageUrl.CopyTo(stream);
            }

            // save the new recipe to the database
            Recipe recipe = new Recipe()
            {
                Title = RecipeDto.Title,
                Description = RecipeDto.Description ?? "",
                Ingredients = RecipeDto.Ingredients,
                Instructions = RecipeDto.Instructions,
                Category = RecipeDto.Category,
                ImageUrl = newImageName,
                CreatedAt = DateTime.Now,
            };

            context.Recipes.Add(recipe);
            context.SaveChanges();

            // clear form

            RecipeDto.Title = "";
            RecipeDto.Description = "";
            RecipeDto.Ingredients = "";
            RecipeDto.Instructions = "";
            RecipeDto.Category = "";
            RecipeDto.ImageUrl = null;

            ModelState.Clear();

            successMessage = "Recipe successfully created";

            Response.Redirect("/Admin/Recipes/Index");
        }
    }
}
