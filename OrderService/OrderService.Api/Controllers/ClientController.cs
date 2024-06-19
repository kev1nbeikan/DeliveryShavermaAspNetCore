using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Contracts.Client;
using OrderService.Api.Extensions;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Models;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("orders/client")]
public class ClientController(IOrderApplicationService orderApplicationService) : ControllerBase
{
    private readonly IOrderApplicationService _orderApplicationService = orderApplicationService;

    [HttpGet("current")]
    public async Task<ActionResult<List<ClientGetCurrent>>> GetCurrent(int filter)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var orders = await _orderApplicationService.GetCurrentOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new ClientGetCurrent(b.Id, b.Status, b.Basket, b.Price, b.Comment,
                b.ClientAddress, b.CourierNumber, b.ClientNumber, b.Cheque));
        return Ok(response);
    }

    [HttpGet("last")]
    public async Task<ActionResult<List<ClientGetLast>>> GetLast(int filter)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var orders = await _orderApplicationService.GetLastOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new ClientGetLast(b.Id, b.Basket, b.Price, b.Comment,
                b.OrderDate, b.DeliveryDate, b.Cheque));
        return Ok(response);
    }

    [HttpGet("canceled")]
    public async Task<ActionResult<List<ClientGetCanceled>>> GetCanceled(int filter)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var orders = await _orderApplicationService.GetCanceledOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new ClientGetCanceled(b.Id, b.Basket, b.Price, b.Comment,
                b.OrderDate, b.Cheque, b.LastStatus, b.ReasonOfCanceled));
        return Ok(response);
    }
}