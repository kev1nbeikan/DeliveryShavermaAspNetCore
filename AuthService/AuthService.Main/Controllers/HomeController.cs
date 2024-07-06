using BarsGroupProjectN1.Core.AppSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthService.Main.Controllers;

public class HomeController : Controller
{
    private readonly IOptions<ServicesOptions> _servicesOptions;

    public HomeController(IOptions<ServicesOptions> servicesOptions)
    {
        _servicesOptions = servicesOptions;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Login", "User");
    }

    public IActionResult UnauthorizedUser()
    {
        HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

        return View(new UnauthorizedUserViewModel
        {
            AuthUrl = _servicesOptions.Value.AuthUrl
        });
    }

    public IActionResult MainLinks()
    {
        return View(_servicesOptions);
    }
}