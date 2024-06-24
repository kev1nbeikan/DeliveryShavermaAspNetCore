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

    [HttpGet("last")]
    public async Task<ActionResult<List<CourierGetLast>>> GetHistory()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var orders = await _orderApplicationService.GetHistoryOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new CourierGetLast(b.Id, b.StoreId, b.Basket, b.Comment, b.DeliveryTime,
                b.OrderDate, b.CookingDate, b.DeliveryDate));
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<ActionResult<CourierGetCurrent>> GetOldestActive()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var order = await _orderApplicationService.GetOldestActive(role, userId);
        if (order is null)
            return NoContent();
        var response = new CourierGetCurrent(order.Id, order.Status, order.Basket, order.Comment,
            order.StoreAddress, order.ClientAddress, order.ClientNumber, order.DeliveryTime);
        return Ok(response);
    }
    
    
    [HttpPut("{orderId:Guid}/delivering")] 
    public async Task<ActionResult> ChangeStatusDelivering(Guid orderId)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());
        
        await _orderApplicationService.ChangeStatusActive(role, Domain.Models.Code.StatusCode.Delivering, userId, orderId);
        return Ok();
    }

    [HttpPut("{orderId:Guid}/waitingClient")] 
    public async Task<ActionResult> ChangeStatusWaitingClient(Guid orderId)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());
        
        await _orderApplicationService.ChangeStatusActive(role, Domain.Models.Code.StatusCode.WaitingClient, userId, orderId);
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