
using Microsoft.EntityFrameworkCore;
using ProductCatalogueWebapi.Entities;

namespace ProductCatalogueWebapi
{
    public class ProjectDbContext:DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options): base(options)
        {}
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}