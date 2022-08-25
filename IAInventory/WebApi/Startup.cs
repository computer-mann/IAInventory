using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Areas.Models.StaffAccount.DbContexts;
using WebApi.Areas.StaffAccount.Models;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using WebApi.Configurations;

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
            //services.AddDbContext<StoreDbContext>();

            services.AddControllers();
            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddIdentity<Staff, IdentityRole<int>>().AddEntityFrameworkStores<StaffDbContext>();

          //  services.AddScoped<DbInitializer>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //  options.DefaultForbidScheme= JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
              .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {

                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateAudience = true,
                      ValidateIssuer = true,
                      ValidateIssuerSigningKey = true,
                      ValidAudience = Configuration["Jwt:Audience"],
                      ValidIssuer = Configuration["Jwt:Issuer"],
                      ValidateLifetime = true,
                      SaveSigninToken = true,
                      RequireExpirationTime = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SignInkey"]))
                  };
                  options.SaveToken = true;

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
