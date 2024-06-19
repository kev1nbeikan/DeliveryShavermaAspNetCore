using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserService.Main.Contracts;
using UserService.Main.Models;

namespace UserService.Main.Controllers;

[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
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

    [HttpPost]
    public IActionResult Bucket([FromBody] List<BucketItem> request)
    {
        return View(
            new BucketViewModel
            {
                Products = request,
                Addresses = ["ул бебры 3", "ул бебры 2", "ул бебры 1"],
                SelectedAddress = "ул бебры 3",
                DefaultComment = "чупопапа"
            }
        );
    }
    
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}