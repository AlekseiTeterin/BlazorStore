using Microsoft.EntityFrameworkCore;
using MyStore.Models;

namespace MyStore.WebApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

    }
}
