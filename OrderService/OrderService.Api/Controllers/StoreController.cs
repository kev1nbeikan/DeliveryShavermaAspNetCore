using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Contracts.Common;
using OrderService.Api.Contracts.Store;
using OrderService.Api.Extensions;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Common.Code;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("orders/store")]
public class StoreController(IOrderApplicationService orderApplicationService) : ControllerBase
{
    private readonly IOrderApplicationService _orderApplicationService = orderApplicationService;

    [HttpGet]
    public async Task<ActionResult<List<StoreGetCurrent>>> GetCurrent()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var orders = (await _orderApplicationService.GetCurrentOrders(role, userId))
                .Where(x => x.Status <= Domain.Common.Code.StatusCode.WaitingCourier).ToList();
        
        if (orders.Count == 0)
            return NoContent();
        
        var response = orders.Select(b =>
            new StoreGetCurrent(b.Id, b.Status, b.Basket, b.Comment,
                b.CourierNumber, b.CookingTime, b.OrderDate));
        return Ok(response);
    }

    [HttpGet("last")]
    public async Task<ActionResult<List<StoreGetLast>>> GetHistory()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var orders = await _orderApplicationService.GetHistoryOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new StoreGetLast(b.Id, b.Basket, b.Comment, b.CookingTime,
                b.OrderDate, b.CookingDate));
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
                b.CourierNumber, b.CookingTime, b.OrderDate));
        return Ok(response);
    }

    [HttpPut("waitingCourier/{orderId:Guid}")]
    public async Task<ActionResult> ChangeStatusWaitingCourier(Guid orderId)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        await _orderApplicationService.ChangeStatusActive(role, Domain.Common.Code.StatusCode.WaitingCourier, userId,
            orderId);
        return Ok();
    }

    [HttpPut("cancel/{orderId:Guid}")]
    public async Task<ActionResult> ChangeStatusCanceled(Guid orderId, [FromBody] CancelOrderRequest cancelOrderRequest)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        await _orderApplicationService.ChangeStatusCanceled(role, userId, orderId, cancelOrderRequest.ReasonOfCanceled);
        return Ok();
    }
}