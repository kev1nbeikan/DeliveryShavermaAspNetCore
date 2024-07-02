using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StoreService.Main.Models;

namespace StoreService.Main.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // return RedirectToAction("Index", "StoreView");
        return RedirectToAction("CurrentOrder", "StoreView");
    }
}