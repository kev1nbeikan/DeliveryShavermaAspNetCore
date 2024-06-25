using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Contracts.Client;
using OrderService.Api.Extensions;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Common.Code;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("orders/client")]
public class ClientController(IOrderApplicationService orderApplicationService, ILogger<ClientController> logger) : ControllerBase
{
    private readonly IOrderApplicationService _orderApplicationService = orderApplicationService;
    private readonly ILogger<ClientController> _logger = logger;

    [HttpGet("current")]
    public async Task<ActionResult<List<ClientGetCurrent>>> GetCurrent()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Request for a list of CURRENT orders. User id = {userId}, role = {role}",
            userId, role);
        
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
        
        _logger.LogInformation(
            "Request for a list of LAST orders. User id = {userId}, role = {role}",
            userId, role);
        
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
        
        _logger.LogInformation(
            "Request for a list of CANCELED orders. User id = {userId}, role = {role}",
            userId, role);
        
        var orders = await _orderApplicationService.GetCanceledOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new ClientGetCanceled(b.Id, b.Basket, b.Price, b.Comment,
                b.OrderDate, b.Cheque, b.LastStatus, b.ReasonOfCanceled));
        return Ok(response);
    }

    [HttpPut("accept/{orderId:Guid}")] 
    public async Task<ActionResult> ChangeStatusAccepted(Guid orderId)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());
        
        _logger.LogInformation(
            "Order Comlete Request. User id = {userId}, role = {role}, orderId = {orderId}",
            userId, role, orderId);
        
        await _orderApplicationService.ChangeStatusCompleted(role, userId, orderId);
        return Ok();
    }
    
    [HttpPut("cancel/{orderId:Guid}")]
    public async Task<ActionResult> ChangeStatusCanceled(Guid orderId, [FromBody] string reasonOfCanceled)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Order Cancellation Request. User id = {userId}, role = {role}, orderId = {orderId}, reasonOfCanceled = {reasonOfCanceled}",
            userId, role, orderId, reasonOfCanceled);
        
        await _orderApplicationService.ChangeStatusCanceled(role, userId, orderId, reasonOfCanceled);
        return Ok();
    }
    
    [HttpGet("test")]
    public async Task<ActionResult<string>> Test()
    {
        return Ok("test Order");
    }
}




