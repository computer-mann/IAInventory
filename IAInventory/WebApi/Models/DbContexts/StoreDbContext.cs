using Microsoft.EntityFrameworkCore;
using WebApi.Models.Store;

namespace WebApi.Models.DbContexts
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Till> Tills { get; set; }

    }
}
