using CourierService.Core.Abstractions;
using CourierService.DataAccess;
using CourierService.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CourierDbContext>(
	options => { options.UseNpgsql(builder.Configuration.GetConnectionString("default")); }
);

builder.Services.AddScoped<ICourierService, CourierService.Application.Services.CourierService>();
builder.Services.AddScoped<ICourierRepository, CourierRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();