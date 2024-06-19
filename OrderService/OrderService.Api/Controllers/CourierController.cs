using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Contracts.Courier;
using OrderService.Api.Extensions;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("orders/courier")]
public class CourierController(IOrderApplicationService orderApplicationService) : ControllerBase
{
    private readonly IOrderApplicationService _orderApplicationService = orderApplicationService;

    [HttpGet("current")]
    public async Task<ActionResult<List<CourierGetCurrent>>> GetCurrent(int filter)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var orders = await _orderApplicationService.GetCurrentOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new CourierGetCurrent(b.Id, b.Status, b.Basket, b.Comment, b.StoreAddress,
                b.ClientAddress, b.ClientNumber, b.DeliveryTime));
        return Ok(response);
    }

    [HttpGet("last")]
    public async Task<ActionResult<List<CourierGetLast>>> GetLast(int filter)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var orders = await _orderApplicationService.GetLastOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new CourierGetLast(b.Id, b.StoreId, b.Basket, b.Comment, b.DeliveryTime,
                b.OrderDate, b.CookingDate, b.DeliveryDate));
        return Ok(response);
    }
}