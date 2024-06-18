using CourierService.API.Contracts;
using CourierService.Core.Abstractions;
using CourierService.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourierService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourierController : Controller
{
	private readonly ICourierService _courierService;

	public CourierController(ICourierService courierService)
	{
		_courierService = courierService;
	}

	[HttpGet]
	public async Task<IActionResult> GetCouriers()
	{
		var couriers = await _courierService.GetAllCouriers();

		var response = couriers.Select(c => new CourierResponse(c.Id, c.Email, c.Password));

		return Ok(response);
	}

	[HttpPost]
	public async Task<IActionResult> CreateCourier([FromBody] CourierRequest request)
	{
		var (courier, error) = Courier.Create(
			Guid.NewGuid(),
			request.email,
			request.password
		);

		if (!string.IsNullOrEmpty(error))
		{
			return BadRequest(error);
		}

		await _courierService.CreateCourier(courier);

		return Ok(courier);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateCourier(Guid id, [FromBody] CourierRequest request)
	{
		return Ok(await _courierService.UpdateCourier(id, request.email, request.password));
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteCourier(Guid id)
	{
		return Ok(await _courierService.DeleteCourier(id));
	}
}