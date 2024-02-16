using Microsoft.EntityFrameworkCore;
using Recipe_Website.Models;

namespace Recipe_Website.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 
        
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
