using OrderService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderService.Application.Service;
using OrderService.DataAccess.Repositories;
using OrderService.Domain.Abstractions;
using OrderService.Api.Extensions;
using OrderService.Api.Middleware;
using OrderService.Domain.Common;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.Configure<ServicesOptions>(builder.Configuration.GetSection("Services"));

// builder.Services.AddServicesHttpClients(builder.Configuration.GetSection("Services").Get<ServicesOptions>());

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

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderServiceDbContext>(
    options =>
    {
        options.UseNpgsql(builder
            .Configuration
            .GetConnectionString(nameof(OrderServiceDbContext)));
    });

builder.Services.AddScoped<IOrderApplicationService, OrderApplicationService>();
builder.Services.AddScoped<ICurrentOrderRepository, CurrentOrderRepository>();
builder.Services.AddScoped<ICanceledOrderRepository, CanceledOrderRepository>();
builder.Services.AddScoped<ILastOrderRepository, LastOrderRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.SetCorsPolicies(app.Services.GetService<IOptions<ServicesOptions>>());

app.UseCors("AllowAllOrigins");

// app.UseMiddleware<UserIdMiddleware>();
// app.UseHttpsRedirection();
app.MapControllers();

app.Run();
