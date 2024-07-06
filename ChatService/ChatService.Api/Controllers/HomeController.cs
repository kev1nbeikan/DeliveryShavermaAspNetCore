using System.Diagnostics;
using BarsGroupProjectN1.Core.AppSettings;
using Microsoft.AspNetCore.Mvc;
using ChatService.Api.Models;
using Microsoft.Extensions.Options;

namespace ChatService.Api.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOptions<ServicesOptions> _servicesOptions;

    public HomeController(ILogger<HomeController> logger, IOptions<ServicesOptions> servicesOptions)
    {
        _logger = logger;
        _servicesOptions = servicesOptions;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Room", "Chat");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}