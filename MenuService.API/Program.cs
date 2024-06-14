using MenuService.Application.Services;
using MenuService.Core.Abstractions;
using MenuService.DataAccess;
using MenuService.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("default");

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

builder.Services.AddCors(
	options =>
	{
		options.AddPolicy(
			"AllowAllOrigins",
			policyBuilder =>
			{
				policyBuilder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
			}
		);
	}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();