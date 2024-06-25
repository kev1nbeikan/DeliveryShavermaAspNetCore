using AuthService.Core;
using AuthService.Core.Abstractions;
using AuthService.Core.Common;
using AuthService.Core.Exceptions;
using AuthService.Core.Extensions;
using AuthService.Main.Contracts;
using AuthService.Main.Infostructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthService.Main.Controllers;

[Route("authService/[controller]")]
public class CourierController : Controller
{
	private readonly ILogger<CourierController> _logger;
	private readonly ICourierAuthService _courierAuthService;
	private readonly IOptions<ServicesOptions> _serviceOptions;

	public CourierController(
		ILogger<CourierController> logger,
		ICourierAuthService courierAuthService,
		IOptions<ServicesOptions> servicesOptions)
	{
		_logger = logger;
		_courierAuthService = courierAuthService;
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
			var userId = await _courierAuthService.Login(request.Login, request.Password);

			var response = new LoginResponse
			{
				UserId = userId.ToString(),
				Role = RoleCode.Courier
			};

			using (var cookiesSaver = CookiesSaverBuilder.ForCourierAuth(Response.Cookies, _serviceOptions))
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
			var user = await _courierAuthService.Register(request.Login, request.Password);
			return Ok(user.Id);
		}
		catch (Exception e) when (e is UniqeConstraitException | e is ArgumentException)
		{
			return BadRequest(e.Message);
		}
	}

	[Authorize]
	[HttpGet("logout")]
	public async Task<IActionResult> Logout()
	{
		await _courierAuthService.Logout();

		return RedirectToAction(nameof(Login));
	}
}