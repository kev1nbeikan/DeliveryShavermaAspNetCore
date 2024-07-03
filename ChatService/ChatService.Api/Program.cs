using BarsGroupProjectN1.Core;
using BarsGroupProjectN1.Core.BackgroundServices;
using BarsGroupProjectN1.Core.Extensions;
using BarsGroupProjectN1.Core.Middlewares;
using ChatService.Api.BackgroundServices;
using ChatService.Api.Hubs;
using ChatService.Core;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();


builder.Services.AddScoped<IChatService, ChatService.Services.ChatService>();

builder.Services.AddHostedService<OrderKafkaConsumerForChatService>();

builder.Services.ConfigureServicesOptions(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseMiddleware<UserIdMiddleware>();

app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();