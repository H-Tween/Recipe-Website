using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recipe_Website.Services;
using Recipe_Website.Models;

namespace Recipe_Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        // pagination
        public int pageIndex = 1;
        public int totalPages = 0;
        private readonly int pageSize = 12;

        // search 
        public string search = "";

        public List<Recipe> Recipes {  get; set; } = new List<Recipe>();

        public IndexModel(ApplicationDbContext context)
        {
            this.context = context; 
        }
        public void OnGet(int? pageIndex, string? search)
        {
            IQueryable<Recipe> query = context.Recipes;

            // search
            if (search != null)
            {
                this.search = search;
                query = query.Where(p => p.Title.Contains(search) || p.Category.Contains(search));
            }

            query = query.OrderByDescending(p => p.Id);

            // pagination
            if (pageIndex == null || pageIndex < 1)
            {
                pageIndex = 1;
            }

            this.pageIndex = (int)pageIndex;

            decimal count = query.Count();
            totalPages = (int)Math.Ceiling(count / pageSize);
            query = query.Skip((this.pageIndex - 1) * pageSize).Take(pageSize);


            Recipes = query.ToList();
        }
    }
}
