using Microsoft.AspNetCore.Mvc;

namespace CourierService.API.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("CourierProfile", "Courier");
    }
}