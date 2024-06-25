using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Handler.Core.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserService.Core.Abstractions;
using UserService.Core.Exceptions;
using UserService.Main.Contracts;
using UserService.Main.Extensions;
using UserService.Main.Models;

namespace UserService.Main.Controllers;

[Route("[controller]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet("{userId:Guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        try
        {
            var user = await _userService.Get(userId);
            return Ok(user);
        }
        catch (NotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("upsert")]
    public async Task<IActionResult> Upsert([FromBody, Required] UpsertUserRequest? request)
    {
        if (!ModelState.IsValid) return BadRequest();
        _logger.LogInformation("Upsert. User requested body = {Request}", request);

        if (request == null) return BadRequest("payload required");

        try
        {
            await _userService.Upsert(request.UserId, request.Address, request.PhoneNumber,
                request.Comment);
            return Ok();
        }
        catch (Exception e) when (
            e is ArgumentException or FailToUpdateRepositoryException<MyUser>)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost("bucket")]
    public async Task<IActionResult> Bucket([FromBody] List<BucketItem> products)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        var viewModel = new BucketViewModel { Products = products };

        try
        {
            var userId = User.UserId();
            _logger.LogInformation("User {UserId} requested bucket", userId);
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
    
    [HttpGet]
    public async Task<IActionResult> Order()
    {
        // if (ModelState.IsValid) return BadRequest();

        var userId = User.UserId();
        
        _logger.LogInformation("User {UserId} requested order", userId);
        
        var httpClient = new HttpClient(); 
        httpClient.BaseAddress = new Uri("http://localhost:5106");  
        var response = await httpClient.GetAsync("orders/client/current"); 
    
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            _logger.LogInformation($"{json}");
            var orders = JsonSerializer.Deserialize<List<OrderGetResponse>>(json);
            if (orders is null)
                return BadRequest("No objects");
            
            var ordersViewModel = new OrdersViewModel { Orders = orders }; 

            return View("Order", ordersViewModel); 
        }
        else
        {
            return BadRequest();
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}