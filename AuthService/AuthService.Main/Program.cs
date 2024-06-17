using AuthService.Core;
using AuthService.DataAccess;
using AuthService.Main.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AuthDbContext>(
    options => { options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(AuthDbContext))); }
);

builder.Services.AddDependenciesServices();

builder.Services.Configure<ServicesOptions>(builder.Configuration.GetSection("Services"));


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();