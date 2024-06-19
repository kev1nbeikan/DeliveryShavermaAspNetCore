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
    
    [HttpGet("current")]
    public async Task<ActionResult<List<StoreGetCurrent>>> GetCurrent(int filter)
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
    public async Task<ActionResult<List<StoreGetLast>>> GetLast(int filter)
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
}