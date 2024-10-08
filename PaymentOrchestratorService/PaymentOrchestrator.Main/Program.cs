using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Extensions;
using BarsGroupProjectN1.Core.Middlewares;
using HandlerService.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDependencies();

builder.Services.Configure<ServicesOptions>(builder.Configuration.GetSection("ServicesOptions"));
builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection("KafkaOptions"));

builder.Services.AddServicesHttpClients(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.SetCorsPolicies(app.Services.GetService<IOptions<ServicesOptions>>()!);


app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<UserIdMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Payment}/{action=Index}/{id?}");


app.Run();