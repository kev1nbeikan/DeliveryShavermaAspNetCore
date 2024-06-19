using Handler.Core.Common;
using HandlerService.DataAccess.Repositories;
using HandlerService.Extensions;
using HandlerService.Middlewares;
using HandlerService.Utils;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDependencies();

builder.Services.Configure<ServicesOptions>(builder.Configuration.GetSection("Services"));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.SetCorsPolicies(app.Services.GetService<IOptions<ServicesOptions>>()!);


app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();


app.UseMiddleware<UserMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Payment}/{action=Index}/{id?}");

app.Run();