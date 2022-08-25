using Microsoft.AspNetCore.Identity;
using WebApi;
using WebApi.Areas.StaffAccount.Models;
using WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleM = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userM = scope.ServiceProvider.GetRequiredService<UserManager<Staff>>();
    new DbInitializer(roleM,userM).popolateDb().Wait();
}

startup.Configure(app,builder.Environment);
// Configure the HTTP request pipeline.
app.MapControllers().RequireAuthorization();

app.Run();




