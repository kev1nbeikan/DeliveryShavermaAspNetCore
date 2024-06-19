using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UserService.DataAccess;
using UserService.Main.Extensions;
using UserService.Main.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<UserDbContext>(
    options => { options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(UserDbContext))); }
);

builder.Services.AddDependencyInjection();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.SetCorsPolicies(app.Services.GetService<IOptions<ServicesOptions>>());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<UserIdMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();