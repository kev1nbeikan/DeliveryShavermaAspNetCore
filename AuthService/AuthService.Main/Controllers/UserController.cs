using AuthService.Core;
using AuthService.Core.Abstractions;
using AuthService.Core.Common;
using AuthService.Core.Exceptions;
using AuthService.Core.Extensions;
using AuthService.Main.Contracts;
using AuthService.Main.Infostructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthService.Main.Controllers;

[Route("authService/[controller]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private IUserAuthService _userAuthService;
    private readonly IOptions<ServicesOptions> _serviceOptions;

    public UserController(ILogger<UserController> logger, IUserAuthService userAuthService,
        IOptions<ServicesOptions> servicesOptions)
    {
        _logger = logger;
        _userAuthService = userAuthService;
        _serviceOptions = servicesOptions;
    }


    [HttpGet("login")]
    public IActionResult UserLoginView()
    {
        return View();
    }

    [HttpGet("Registration")]
    public IActionResult UserRegisterView()
    {
        return View();
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthUserLoginRequest request)
    {
        try
        {
            var userId = await _userAuthService.Login(request.Email, request.Password);


            var response = new LoginResponse
            {
                UserId = userId.ToString(),
                Role = RoleCode.Client
            };

            using (var cookiesSaver = CookiesSaverBuilder.ForUserAuth(Response.Cookies, _serviceOptions))
            {
                cookiesSaver.Append(nameof(response.UserId), response.UserId);
                cookiesSaver.Append(nameof(response.Role), response.Role.Value.ToIntString());
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

    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthUserLoginRequest request)
    {
        try
        {
            var user = await _userAuthService.Register(request.Email, request.Password);
            return Ok(user.Id);
        }
        catch (Exception e) when (e is UniqeConstraitException | e is ArgumentException)
        {
            return BadRequest(e.Message);
        }
    }
}