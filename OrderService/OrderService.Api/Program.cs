using System.Reflection;
using System.Xml.XPath;
using BarsGroupProjectN1.Core.Extensions;
using BarsGroupProjectN1.Core.Middlewares;
using OrderService.DataAccess;
using Microsoft.EntityFrameworkCore;
using OrderService.Api.Extensions;
using OrderService.Api.Middleware;
using OrderService.Application.Service;
using OrderService.DataAccess.Repositories;
using OrderService.Domain.Abstractions;
using OrderService.Application;
using Swashbuckle.AspNetCore.SwaggerGen;

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
builder.Services.AddSwaggerGen(AddSwaggerCommentsFromXml.IncludeSwaggerCommentsFromXml);

builder.Services.AddDbContext<OrderServiceDbContext>(
    options =>
    {
        options.UseNpgsql(builder
            .Configuration
            .GetConnectionString(nameof(OrderServiceDbContext)));
    });


builder.Services.ConfigureKafkaOptions(builder.Configuration);

builder.Services.AddScoped<ExceptionMiddleware>(); 

builder.Services.AddScoped<IOrderApplicationService, OrderApplicationService>();
builder.Services.AddScoped<ICurrentOrderRepository, CurrentOrderRepository>();
builder.Services.AddScoped<ICanceledOrderRepository, CanceledOrderRepository>();
builder.Services.AddScoped<ILastOrderRepository, LastOrderRepository>();
builder.Services.AddScoped<IOrderPublisher, OrderPublisher>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.SetCorsPolicies(app.Services.GetService<IOptions<ServicesOptions>>());

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowAllOrigins");

app.UseMiddleware<UserIdMiddleware>();
app.MapControllers();

app.Run();
