using Microsoft.AspNetCore.Identity;
using WebApi.Areas.Models;
using WebApi.Areas.Models.DbContexts;
using WebApi.Areas.Models.StaffAccount.DbContexts;
using WebApi.Areas.StaffAccount.Models;

namespace WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddDbContext<StaffDbContext>();
            //services.AddDbContext<StoreDbContext>();

            services.AddControllers();
            //services.AddAuthentication();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddIdentity<Staff, IdentityRole<int>>().AddEntityFrameworkStores<StaffDbContext>();
            


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
        }

        public void Configure(IApplicationBuilder app,IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

          
        }
    }
}
