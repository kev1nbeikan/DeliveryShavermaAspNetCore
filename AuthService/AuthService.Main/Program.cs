using AuthService.Core;
using AuthService.DataAccess;
using AuthService.Main.Extensions;
using BarsGroupProjectN1.Core.AppSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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

// app.SetCorsPolicies(app.Services.GetService<IOptions<ServicesOptions>>()!);

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());


app.UseStaticFiles();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();