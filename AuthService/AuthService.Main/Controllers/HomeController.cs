using Microsoft.AspNetCore.Mvc;

namespace AuthService.Main.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Login", "User");
    }
}