using BarsGroupProjectN1.Core.Extensions;
using BarsGroupProjectN1.Core.Models;
using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Contracts.Common;
using OrderService.Api.Contracts.Store;
using OrderService.Api.Extensions;
using OrderService.Domain.Abstractions;
using Core = BarsGroupProjectN1.Core.Models.Order;

namespace OrderService.Api.Controllers;

/// <summary>
/// Контроллер, отвечающий за обработку запросов магазина, связанных с заказами.
/// </summary>
/// <param name="orderApplicationService">Сервис, предоставляющий возможность работы с заказами.</param>
/// <param name="logger">Сервис, предоставляющий возможность логгирования.</param>
[ApiController]
[Route("orders/store")]
public class StoreController(IOrderApplicationService orderApplicationService, ILogger<StoreController> logger) : ControllerBase
{
    private readonly IOrderApplicationService _orderApplicationService = orderApplicationService;
    private readonly ILogger<StoreController> _logger = logger;

    /// <summary>
    /// Возвращает список текущих заказов магазина.
    /// </summary>
    /// <returns>Список текущих заказов магазина.</returns>
    [HttpGet]
    public async Task<ActionResult<List<StoreGetCurrent>>> GetCurrent()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Запрос магазина на получение текущих заказов. User id = {userId}, role = {role}",
            userId, role);
        
        var orders = (await _orderApplicationService.GetStoreCurrentOrders(role, userId))
            .Where(x => x.Status <= Core.StatusCode.WaitingCourier).ToList();

        if (orders.Count == 0)
            return NoContent();

        var response = orders.Select(b =>
            new StoreGetCurrent(b.Id, b.ClientId, b.Status, b.Basket, b.Comment,
                b.CourierNumber, b.CookingTime, b.OrderDate));
        return Ok(response);
    }

    /// <summary>
    /// Возвращает список истории заказов магазина.
    /// </summary>
    /// <returns>Список истории заказов магазина.</returns>
    [HttpGet("last")]
    public async Task<ActionResult<List<StoreGetLast>>> GetHistory()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Запрос магазина на получение истории заказов. User id = {userId}, role = {role}",
            userId, role);
        
        var orders = await _orderApplicationService.GetHistoryOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new StoreGetLast(b.Id, b.Basket, b.Comment, b.CookingTime,
                b.OrderDate, b.CookingDate));
        return Ok(response);
    }

    /// <summary>
    /// Возвращает список отмененных заказов магазина.
    /// </summary>
    /// <returns>Список отмененных заказов магазина.</returns>
    [HttpGet("canceled")]
    public async Task<ActionResult<List<StoreGetCanceled>>> GetCanceled()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());
    
        _logger.LogInformation(
            "Запрос магазина на получение отмененных заказов. User id = {userId}, role = {role}",
            userId, role);
    
        var orders = await _orderApplicationService.GetCanceledOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new StoreGetCanceled(b.Id, b.Basket, b.Comment, b.OrderDate, b.CanceledDate,
                b.LastStatus, b.ReasonOfCanceled, b.WhoCanceled));
        return Ok(response);
    }
    
    /// <summary>
    /// Возвращает список новых заказов магазина, созданных после указанной даты.
    /// </summary>
    /// <param name="lastOrderDate">Дата, после которой нужно получить заказы.</param>
    /// <returns>Список новых заказов магазина, созданных после указанной даты.</returns>
    [HttpGet("getNewOrders/{lastOrderDate:Datetime}")]
    public async Task<ActionResult<List<StoreGetCurrent>>> GetNewOrderByDate(DateTime lastOrderDate)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Запрос магазина на получение новых заказов по дате. User id = {userId}, role = {role}, lastOrderDate = {lastOrderDate}",
            userId, role, lastOrderDate);
        
        var orders = await _orderApplicationService.GetNewOrdersByDate(role, userId, lastOrderDate);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new StoreGetCurrent(b.Id,  b.ClientId, b.Status, b.Basket, b.Comment,
                b.CourierNumber, b.CookingTime, b.OrderDate));
        return Ok(response);
    }

    /// <summary>
    /// Изменяет статус заказа на "Ожидание курьера".
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <returns>Статус-код 200 (Ok).</returns>
    [HttpPut("waitingCourier/{orderId:Guid}")]
    public async Task<ActionResult> ChangeStatusWaitingCourier(Guid orderId)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());
        
        _logger.LogInformation(
            "Запрос магазина на изменение статуса заказа на waitingCourier. User id = {userId}, role = {role}, orderId = {orderId}",
            userId, role, orderId);
        
        await _orderApplicationService.ChangeStatusActive(role, Core.StatusCode.WaitingCourier, userId,
            orderId);
        return Ok();
    }

    /// <summary>
    /// Изменяет статус заказа на "Отменен".
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <param name="cancelOrderRequest">Запрос магазина на отмену заказа. <see cref="CancelOrderRequest"/></param>
    /// <returns>Статус-код 200 (Ok).</returns>
    [HttpPut("cancel/{orderId:Guid}")]
    public async Task<ActionResult> ChangeStatusCanceled(Guid orderId, [FromBody] CancelOrderRequest cancelOrderRequest)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Запрос магазина на отмену заказа. User id = {userId}, role = {role}, orderId = {orderId}, reasonOfCanceled = {reasonOfCanceled}",
            userId, role, orderId, cancelOrderRequest.ReasonOfCanceled);

        await _orderApplicationService.ChangeStatusCanceled(role, userId, orderId, cancelOrderRequest.ReasonOfCanceled);
        return Ok();
    }
}