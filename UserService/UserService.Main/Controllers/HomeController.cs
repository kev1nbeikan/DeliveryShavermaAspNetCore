using Microsoft.AspNetCore.Mvc;

namespace UserService.Main.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("CurrentOrder", "User");
    }
}