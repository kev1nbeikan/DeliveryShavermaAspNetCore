using AuthService.Application.Services;
using AuthService.Core;
using AuthService.Core.Abstractions;
using AuthService.Core.Exceptions;
using AuthService.Main.Contracts;
using AuthService.Main.Infostructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthService.Main.Controllers;

[Route("authService/[controller]")]
public class StoreController : Controller
{
    private readonly ILogger<StoreController> _logger;
    private readonly IStoreAuthService _storeAuthService;
    private readonly IOptions<ServicesOptions> _serviceOptions;

    public StoreController(ILogger<StoreController> logger, IStoreAuthService storeAuthService,
        IOptions<ServicesOptions> servicesOptions)
    {
        _logger = logger;
        _storeAuthService = storeAuthService;
        _serviceOptions = servicesOptions;
    }


    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }
    
    


    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthStaffRequest request)
    {
        try
        {
            var userId = await _storeAuthService.Login(request.Login, request.Password);


            var response = new LoginResponse
            {
                UserId = userId.ToString(),
                Role = "store"
            };

            using (var cookiesSaver = CookiesSaverBuilder.ForCourierAuth(Response.Cookies, _serviceOptions))
            {
                cookiesSaver.Append(nameof(response.UserId), response.UserId);
                cookiesSaver.Append(nameof(response.Role), response.Role);
            }

            return Ok(response);
        }
        catch (Exception e) when (
            e is NotFoundException ||
            e is IncorectPasswordException
        )
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthStaffRequest request)
    {
        try
        {
            var user = await _storeAuthService.Register(request.Login, request.Password);
            return Ok(user.Id);
        }
        catch (Exception e) when (e is UniqeConstraitException | e is ArgumentException)
        {
            return BadRequest(e.Message);
        }
    }
}