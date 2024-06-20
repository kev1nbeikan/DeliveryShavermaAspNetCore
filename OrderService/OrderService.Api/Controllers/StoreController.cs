using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Contracts.Store;
using OrderService.Api.Extensions;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("orders/store")]
public class StoreController(IOrderApplicationService orderApplicationService) : ControllerBase
{
    private readonly IOrderApplicationService _orderApplicationService = orderApplicationService;
    [HttpGet]
    [HttpGet("current")]
    public async Task<ActionResult<List<StoreGetCurrent>>> GetCurrent()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var orders = await _orderApplicationService.GetCurrentOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new StoreGetCurrent(b.Id, b.Status, b.Basket, b.Comment,
                b.CourierNumber, b.CookingTime));
        return Ok(response);
    }

    [HttpGet("last")]
    public async Task<ActionResult<List<StoreGetLast>>> GetLast()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var orders = await _orderApplicationService.GetLastOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new StoreGetLast(b.Id, b.Basket, b.Comment, b.CookingTime,
                b.OrderDate, b.CookingDate));
        return Ok(response);
    }

    [HttpGet("latest_order")]
    public async Task<ActionResult<StoreGetCurrent>> GetLatestOrder()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var order = await _orderApplicationService.GetNewestOrder(role, userId);
        var response = new StoreGetCurrent(order.Id, order.Status, order.Basket, order.Comment,
            order.ClientNumber, order.DeliveryTime);
        return Ok(response);
    }
    
    [HttpGet("get_new_orders/{lastOrderDate:Datetime}")]
    public async Task<ActionResult<List<StoreGetCurrent>>> GetNewOrderByDate(DateTime lastOrderDate)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var orders = await _orderApplicationService.GetNewOrdersByDate(role, userId, lastOrderDate);
        var response = orders.Select(b =>
            new StoreGetCurrent(b.Id, b.Status, b.Basket, b.Comment,
                b.CourierNumber, b.CookingTime));
        return Ok(response);
    }
    
    [HttpPut("{orderId:Guid}/waitingCourier")] 
    public async Task<ActionResult> ChangeStatusWaitingCourier(Guid orderId)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());
        
        await _orderApplicationService.ChangeStatusActive(role, Domain.Models.Code.StatusCode.WaitingCourier, userId, orderId);
        return Ok();
    }
}