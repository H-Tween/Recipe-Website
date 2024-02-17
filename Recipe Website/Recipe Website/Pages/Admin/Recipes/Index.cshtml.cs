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

        // pagination
        public int pageIndex = 1;
        public int totalPages = 0;
        private readonly int pageSize = 9;

        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void OnGet(int? pageIndex)
        {
            IQueryable<Recipe> query = context.Recipes;
            query = query.OrderByDescending(p => p.Id);

            //pagination
            if (pageIndex == null || pageIndex < 1)
            {
                pageIndex = 1;
            }

            this.pageIndex = (int)pageIndex;

            decimal count = query.Count();
            totalPages = (int)Math.Ceiling(count / pageSize);
            query = query.Skip((this.pageIndex - 1) * pageSize).Take(pageSize);


            Recipes =query.ToList();

        }
    }
}
