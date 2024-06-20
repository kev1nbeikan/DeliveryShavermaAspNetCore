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
    [HttpGet]
    [HttpGet("current")]
    public async Task<ActionResult<List<CourierGetCurrent>>> GetCurrent()
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
    public async Task<ActionResult<List<CourierGetLast>>> GetLast()
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
    
    [HttpGet("latest_order")]
    public async Task<ActionResult<CourierGetCurrent>> GetLatestOrder()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var order = await _orderApplicationService.GetNewestOrder(role, userId);
        var response = new CourierGetCurrent(order.Id, order.Status, order.Basket, order.Comment,
            order.StoreAddress, order.ClientAddress, order.ClientNumber, order.DeliveryTime);
        return Ok(response);
    }
    
    [HttpGet("get_new_orders/{lastOrderDate:Datetime}")]
    public async Task<ActionResult<List<CourierGetCurrent>>> GetNewOrderByDate(DateTime lastOrderDate)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var orders = await _orderApplicationService.GetNewOrdersByDate(role, userId, lastOrderDate);
        var response = orders.Select(b =>
            new CourierGetCurrent(b.Id, b.Status, b.Basket, b.Comment, b.StoreAddress,
                b.ClientAddress, b.ClientNumber, b.DeliveryTime));
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
}