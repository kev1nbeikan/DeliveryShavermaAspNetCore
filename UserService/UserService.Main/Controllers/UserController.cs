using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserService.Core.abstractions;
using UserService.Core.Exceptions;
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


    [HttpPost("AddNewOrUpdate")]
    public async Task<IActionResult> AddNewOrUpdate([FromBody] AddNewUserOrUpdateRequest userFields)
    {
        await _userService.AddNewOrUpdate(userFields.UserId, userFields.Address, userFields.PhoneNumber,
            userFields.Comment);
        return Ok();
    }


    // [HttpPost("Bucket")]
    public async Task<IActionResult> Bucket([FromBody] List<BucketItem> request)
    {
        try
        {
            var userId = User.UserId();
            _logger.LogInformation("User {userId} requested bucket", userId);
            var user = await _userService.Get(userId);

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
        catch (NotFoundException e)
        {
            return View(
                new BucketViewModel
                {
                    Products = request,
                    Addresses = [],
                    SelectedAddress = null,
                    DefaultComment = ""
                }
            );
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}