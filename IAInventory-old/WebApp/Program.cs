using Microsoft.AspNetCore.Identity;
using WebApp;
using WebApp.Areas.Accounts.Models;
using WebApp.Configuration;

var builder = WebApplication.CreateBuilder(args);


var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleM = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userM = scope.ServiceProvider.GetRequiredService<UserManager<Staff>>();
    new DbInitializer(roleM, userM).popolateDb().Wait();
}

startup.Configure(app, builder.Environment);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
