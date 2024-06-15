using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AuthService.Main.Models;

namespace AuthService.Main.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult AuthUser()
    {
        return View();
    }
    
    public IActionResult AuthStore()
    {
        return View();
    }
    
    public IActionResult AuthCurier()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}