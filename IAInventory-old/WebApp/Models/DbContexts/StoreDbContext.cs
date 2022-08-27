using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Accounts.Models;

namespace WebApp.Models.DbContexts
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        
        public DbSet<WebApp.Areas.Accounts.Models.Staff>? Staff { get; set; }

    }
}
