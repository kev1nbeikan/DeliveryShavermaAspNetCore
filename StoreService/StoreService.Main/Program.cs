using BarsGroupProjectN1.Core.Extensions;
using BarsGroupProjectN1.Core.Middlewares;
using Microsoft.EntityFrameworkCore;
using StoreService.DataAccess;
using StoreService.Main.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StoreDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(StoreDbContext)));
    }
);


builder.Services.ConfigureServicesOptions(builder.Configuration);
builder.Services.AddServicesHttpClients(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddDi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");


    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseStaticFiles();

app.UseMiddleware<UserIdMiddleware>();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
);

app.Run();