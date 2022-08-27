using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Areas.Models.StaffAccount.DbContexts;
using WebApi.Areas.StaffAccount.Models;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using WebApi.Configurations;
using WebApi.Services;
using WebApi.Models.DbContexts;

namespace WebApi
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
            services.AddDbContext<StaffDbContext>(dbContextOptions => dbContextOptions
                .UseMySql(Configuration.GetConnectionString("mysql"),new MySqlServerVersion(new Version(8,0,29))));

            services.AddDbContext<StoreDbContext>(dbContextOptions => dbContextOptions
                .UseMySql(Configuration.GetConnectionString("mysql"), new MySqlServerVersion(new Version(8, 0, 29))));

            services.AddControllers();
            
            
            
            services.AddIdentity<Staff, IdentityRole<int>>().AddEntityFrameworkStores<StaffDbContext>();

            services.AddTransient<IJwtUtils,JwtUtils>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
            services.Configure<AppSettings>(Configuration.GetSection("Jwt"));
            services.AddControllersWithViews();
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
