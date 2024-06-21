using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Handler.Core.Common;
using Microsoft.AspNetCore.Mvc;
using UserService.Core.Abstractions;
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
    public async Task<IActionResult> AddNewOrUpdate([FromBody, Required] UpsertUserRequest request)
    {
        _logger.LogInformation($"AddNewOrUpdateCalled. User requested AddNewOrUpdate {request}");

        if (request == null)
        {
            return Ok();
        }

        try
        {
            await _userService.Upsert(request.UserId, request.Address, request.PhoneNumber,
                request.Comment);
            return Ok();
        }
        catch (Exception e) when (
            e is ArgumentException or FailToUpdateRepositoryException<MyUser>)
        {
            return Ok(e.Message);
        }
    }


    // [HttpPost("Bucket")]
    public async Task<IActionResult> Bucket([FromBody] List<BucketItem> products)
    {
        var viewModel = new BucketViewModel { Products = products };

        try
        {
            var userId = User.UserId();
            _logger.LogInformation("User {userId} requested bucket", userId);
            var user = await _userService.Get(userId);

            viewModel.Addresses = user.Addresses;
            viewModel.PhoneNumber = user.PhoneNumber;
            viewModel.DefaultComment = user.Comment;
            viewModel.SelectedAddress = user.Addresses.First();

            return View(viewModel);
        }
        catch (NotFoundException e)
        {
            return View(viewModel);
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}