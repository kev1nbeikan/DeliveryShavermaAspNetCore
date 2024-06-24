using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Contracts.Client;
using OrderService.Api.Extensions;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("orders/client")]
public class ClientController(IOrderApplicationService orderApplicationService) : ControllerBase
{
    private readonly IOrderApplicationService _orderApplicationService = orderApplicationService;

    [HttpGet("current")]
    public async Task<ActionResult<List<ClientGetCurrent>>> GetCurrent()
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
    public async Task<ActionResult<List<ClientGetLast>>> GetHistory()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var orders = await _orderApplicationService.GetHistoryOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new ClientGetLast(b.Id, b.Basket, b.Price, b.Comment,
                b.OrderDate, b.DeliveryDate, b.Cheque));
        return Ok(response);
    }

    [HttpGet("canceled")]
    public async Task<ActionResult<List<ClientGetCanceled>>> GetCanceled()
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

    [HttpPut("{orderId:Guid}/accepted")] 
    public async Task<ActionResult> ChangeStatusAccepted(Guid orderId)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());
        
        await _orderApplicationService.ChangeStatusCompleted(role, userId, orderId);
        return Ok();
    }
    
    [HttpPut("{orderId:Guid}/canceled")]
    public async Task<ActionResult> ChangeStatusCanceled(Guid orderId, [FromBody] string reasonOfCanceled)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());
        
        await _orderApplicationService.ChangeStatusCanceled(role, userId, orderId, reasonOfCanceled);
        return Ok();
    }
}




