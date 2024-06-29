using System.Diagnostics;
using MenuService.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MenuService.API.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("ProductsMenu", "Product");
    }
}