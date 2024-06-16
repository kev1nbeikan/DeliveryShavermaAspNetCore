using MenuService.Application.Services;
using MenuService.Core.Abstractions;
using MenuService.DataAccess;
using MenuService.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ProductDbContext>(
	options => { options.UseNpgsql(connectionString); }
);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add services to the container.
// builder.Services.AddControllersWithViews();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();


app.UseRouting();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}"
);

app.Run();