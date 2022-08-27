using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Accounts.Models;
using WebApp.Areas.Accounts.Models.DbContexts;
using WebApp.Models.DbContexts;

namespace WebApp
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddDbContext<StoreDbContext>(dbContextOptions => dbContextOptions
                .UseMySql(Configuration.GetConnectionString("mysql"), new MySqlServerVersion(new Version(8, 0, 29))));

            services.AddDbContext<StaffDbContext>(dbContextOptions => dbContextOptions
                .UseMySql(Configuration.GetConnectionString("mysql"), new MySqlServerVersion(new Version(8, 0, 29))));

            services.AddControllers();

            services.AddIdentity<Staff, IdentityRole<int>>().AddEntityFrameworkStores<StaffDbContext>();
            

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/access-denied";
            });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

        }
    }
}
