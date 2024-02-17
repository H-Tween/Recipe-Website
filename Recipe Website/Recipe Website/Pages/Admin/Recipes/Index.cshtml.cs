using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recipe_Website.Services;
using System.Net.Http.Headers;
using Recipe_Website.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Recipe_Website.Pages.Admin.Recipes
{
    public class IndexModel : PageModel
    {
        public readonly ApplicationDbContext context;

        // pagination
        public int pageIndex = 1;
        public int totalPages = 0;
        private readonly int pageSize = 5;

        // search
        public string search = "";

        // sort
        public string column = "Id";
        public string orderBy = "desc";

        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void OnGet(int? pageIndex, string? search, string? column, string? orderBy)
        {
            IQueryable<Recipe> query = context.Recipes;

            if (search != null)
            {
                this.search = search;
                query = query.Where(p => p.Title.Contains(search) || p.Category.Contains(search));
            }

            //sort
            string[] validColumns = { "Id", "Title", "Category", "CreatedAt" };
            string[] validOrderBy = { "desc", "asc" };

            if (!validColumns.Contains(column))
            {
                column = "Id";
            }

            if (!validOrderBy.Contains(orderBy))
            {
                orderBy = "desc";
            }

            this.column = column;
            this.orderBy = orderBy;

            if (column == "Title")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Title);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Title);
                }
            }

            else if (column == "Category")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Category);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Category);
                }
            }

            else if (column == "CreatedAt")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.CreatedAt);
                }
                else
                {
                    query = query.OrderByDescending(p => p.CreatedAt);
                }
            }
            else 
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Id);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Id);
                }
            }


            // query = query.OrderByDescending(p => p.Id);

            //pagination
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
