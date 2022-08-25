using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.DbContexts
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options)
        {

        }
        //public DbSet MyProperty { get; set; }
        //public DbSet MyProperty { get; set; }
        //public DbSet MyProperty { get; set; }
        //public DbSet MyProperty { get; set; }
        //public DbSet MyProperty { get; set; }
        //public DbSet MyProperty { get; set; }
        //public DbSet MyProperty { get; set; }

    }
}
