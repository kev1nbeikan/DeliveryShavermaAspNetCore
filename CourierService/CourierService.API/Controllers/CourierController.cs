﻿using CourierService.API.Contracts;
using CourierService.API.Models;
using CourierService.Core.Abstractions;
using CourierService.Core.Models;
using CourierService.Core.Models.Code;
using Microsoft.AspNetCore.Mvc;

namespace CourierService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourierController : Controller
{
	private readonly ICourierService _courierService;

	private readonly IOrdersApiClient _ordersApiClient;

	public CourierController(ICourierService courierService, IOrdersApiClient ordersApiClient)
	{
		_courierService = courierService;
		_ordersApiClient = ordersApiClient;
	}

	[HttpGet]
	public async Task<IActionResult> GetCouriers()
	{
		var couriers = await _courierService.GetAllCouriers();

		var response = couriers.Select(c => new CourierResponse(c.Id, c.Email, c.Password, c.Status));

		return Ok(response);
	}

	[HttpGet("getcourierbyid")]
	public async Task<IActionResult> GetCourierById([FromQuery] CourierListRequest request)
	{
		var couriers = await _courierService.GetAllCouriers();

		var response = couriers.Where(p => request.Guids.Contains(p.Id)).ToList();

		return Ok(response);
	}

	[HttpPost]
	public async Task<IActionResult> CreateCourier([FromBody] CourierRequest request)
	{
		var (courier, error) = Courier.Create(
			Guid.NewGuid(),
			request.email,
			request.password,
			status: default
		);

		if (!string.IsNullOrEmpty(error))
		{
			return BadRequest(error);
		}

		await _courierService.CreateCourier(courier);

		return Ok(courier);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateCourier(Guid id, string email, string password)
	{
		return Ok(await _courierService.UpdateCourier(id, email, password));
	}

	[HttpPost("status/{id:guid}")]
	public async Task<IActionResult> UpdateCourierStatus(Guid id, CourierStatusCode status)
	{
		await _courierService.UpdateCourier(id, status);

		return RedirectToAction(nameof(CourierProfile));
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteCourier(Guid id)
	{
		return Ok(await _courierService.DeleteCourier(id));
	}

	[HttpGet("orders/courier/last")]
	public async Task<IActionResult> GetLastOrder()
	{
		try
		{
			var latestOrder = _ordersApiClient.GetLatestOrderAsync();
			return Ok(latestOrder);
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Ошибка сервера: {ex.Message}");
		}
	}

	[HttpGet("orders/order_id/status")]
	public async Task<IActionResult> GetOrderStatus()
	{
		try
		{
			var orders = await _ordersApiClient.GetCurrentOrdersAsync();

			var firstOrderStatus = orders.FirstOrDefault()?.Status;

			return Ok(firstOrderStatus);
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
		}
	}

	[HttpGet("profile")]
	public async Task<IActionResult> CourierProfile()
	{
		return View(new CourierViewModel());
	}
}