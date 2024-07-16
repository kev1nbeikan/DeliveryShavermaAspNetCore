using BarsGroupProjectN1.Core.Extensions;
using BarsGroupProjectN1.Core.Models;
using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Contracts.Common;
using OrderService.Api.Contracts.Courier;
using OrderService.Api.Extensions;
using OrderService.Domain.Abstractions;
using Core = BarsGroupProjectN1.Core.Models.Order;

namespace OrderService.Api.Controllers;

/// <summary>
/// Контроллер, отвечающий за обработку запросов курьера, связанных с заказами.
/// </summary>
/// <param name="orderApplicationService">Сервис, предоставляющий возможность работы с заказами.</param>
/// <param name="logger">Сервис, предоставляющий возможность логгирования.</param>
[ApiController]
[Route("orders/courier")]
public class CourierController(IOrderApplicationService orderApplicationService, ILogger<CourierController> logger)
    : ControllerBase
{
    private readonly IOrderApplicationService _orderApplicationService = orderApplicationService;
    private readonly ILogger<CourierController> _logger = logger;

    /// <summary>
    /// Возвращает самый старый активный заказ курьера.
    /// </summary>
    /// <returns>Самый старый активный заказ курьера.</returns>
    [HttpGet]
    public async Task<ActionResult<CourierGetCurrent>> GetOldestActive()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Запрос курьера на получение последнего активного заказа. User id = {userId}, role = {role}",
            userId, role);
        
        var order = await _orderApplicationService.GetOldestActive(role, userId);
        if (order is null)
            return NoContent();
        var response = new CourierGetCurrent(order.Id, order.ClientId, order.Status, order.Basket, order.Comment,
            order.StoreAddress, order.ClientAddress, order.ClientNumber, order.DeliveryTime, order.CookingTime);
        return Ok(response);
    }

    /// <summary>
    /// Возвращает историю заказов курьера.
    /// </summary>
    /// <returns>Список историю заказов курьера.</returns>
    [HttpGet("last")]
    public async Task<ActionResult<List<CourierGetLast>>> GetHistory()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Запрос курьера на получение списка истории заказов. User id = {userId}, role = {role}",
            userId, role);
        
        var orders = await _orderApplicationService.GetHistoryOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new CourierGetLast(b.Id, b.StoreId, b.Basket, b.Comment, b.DeliveryTime,
                b.OrderDate, b.CookingDate, b.DeliveryDate));
        return Ok(response);
    }
    
    /// <summary>
    /// Возвращает список отмененных заказов курьера.
    /// </summary>
    /// <returns>Список отмененных заказов курьера.</returns>
    [HttpGet("canceled")]
    public async Task<ActionResult<List<CourierGetCanceled>>> GetCanceled()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());
    
        _logger.LogInformation(
            "Запрос курьера на получение отмененных заказов. User id = {userId}, role = {role}",
            userId, role);
    
        var orders = await _orderApplicationService.GetCanceledOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new CourierGetCanceled(b.Id, b.Basket, b.Comment, b.OrderDate, b.CanceledDate,
                b.LastStatus, b.ReasonOfCanceled, b.WhoCanceled));
        return Ok(response);
    }
    
    /// <summary>
    /// Изменяет статус заказа на "В доставке".
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <returns>Статус-код 200 (Ok).</returns>
    [HttpPut("delivering/{orderId:Guid}")]
    public async Task<ActionResult> ChangeStatusDelivering(Guid orderId)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Запрос курьера на изменение статуса заказа на delivering. User id = {userId}, role = {role}, orderId = {orderId}",
            userId, role, orderId);
        
        await _orderApplicationService.ChangeStatusActive(role, Core.StatusCode.Delivering, userId, orderId);
        return Ok();
    }

    /// <summary>
    /// Изменяет статус заказа на "Ожидает клиента".
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <returns>Статус-код 200 (Ok).</returns>
    [HttpPut("waitingClient/{orderId:Guid}")]
    public async Task<ActionResult> ChangeStatusWaitingClient(Guid orderId)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Запрос курьера на изменение статуса заказа на waitingClient. User id = {userId}, role = {role}, orderId = {orderId}",
            userId, role, orderId);
        
        await _orderApplicationService.ChangeStatusActive(role, Core.StatusCode.WaitingClient, userId, orderId);
        return Ok();
    }

    /// <summary>
    /// Изменяет статус заказа на "Отменен".
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <param name="cancelOrderRequest">Причина отмена заказа.<see cref="CancelOrderRequest"/></param>
    /// <returns>Статус-код 200 (Ok).</returns>
    [HttpPut("cancel/{orderId:Guid}")]
    public async Task<ActionResult> ChangeStatusCanceled(Guid orderId, [FromBody] CancelOrderRequest cancelOrderRequest)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());
        
        _logger.LogInformation(
            "Запрос курьера на отмену заказа. User id = {userId}, role = {role}, orderId = {orderId}, reasonOfCanceled = {reasonOfCanceled}",
            userId, role, orderId, cancelOrderRequest.ReasonOfCanceled);
        
        await _orderApplicationService.ChangeStatusCanceled(role, userId, orderId, cancelOrderRequest.ReasonOfCanceled);
        return Ok();
    }
}