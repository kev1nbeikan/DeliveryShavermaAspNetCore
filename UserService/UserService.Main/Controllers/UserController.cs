using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserService.Core;
using UserService.Main.Contracts;
using UserService.Main.Extensions;
using UserService.Main.Models;

namespace UserService.Main.Controllers;

[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }


    [HttpPost]
    public async Task<IActionResult> AddNewOrUpdate([FromBody] AddNewUserOrUpdateRequest user)
    {
        
        await _userService.AddNewOrUpdate(user);
        return Ok();
    }
    

    [HttpPost("Bucket")]
    public async Task<IActionResult> Bucket([FromBody] List<BucketItem> request)
    {
        
        var user = await _userService.Get(User.UserId());

        return View(
            new BucketViewModel
            {
                Products = request,
                Addresses = user.Addresses,
                SelectedAddress = user.Addresses.Last(),
                DefaultComment = user.Comment
            }
        );
    }
    
    


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public interface IUserService
{
    Task<MyUser> Get(object userId);
    Task AddNewOrUpdate(AddNewUserOrUpdateRequest user);
}