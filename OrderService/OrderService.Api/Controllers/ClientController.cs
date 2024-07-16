using BarsGroupProjectN1.Core.Extensions;
using BarsGroupProjectN1.Core.Models;
using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Contracts.Client;
using OrderService.Api.Contracts.Common;
using OrderService.Domain.Abstractions;

namespace OrderService.Api.Controllers;

/// <summary>
/// Контроллер, отвечающий за обработку запросов клиента, связанных с заказами.
/// </summary>
/// <param name="orderApplicationService">Сервис, предоставляющий возможность работы с заказами.</param>
/// <param name="logger">Сервис, предоставляющий возможность логгирования.</param>
[ApiController]
[Route("orders/client")]
public class ClientController(IOrderApplicationService orderApplicationService, ILogger<ClientController> logger)
    : ControllerBase
{
    private readonly IOrderApplicationService _orderApplicationService = orderApplicationService;
    private readonly ILogger<ClientController> _logger = logger;

    /// <summary>
    /// Возвращает список текущих заказов клиента
    /// </summary> 
    /// <returns>Список текущих заказов клиента</returns>
    [HttpGet("current")]
    public async Task<ActionResult<List<ClientGetCurrent>>> GetCurrent()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Запрос клиента на получение текущих заказ. User id = {userId}, role = {role}",
            userId, role);

        var orders = await _orderApplicationService.GetCurrentOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new ClientGetCurrent(b.Id, b.ClientId, b.StoreId, b.CourierId, b.Status, b.Basket,
                b.Price, b.Comment, b.ClientAddress, b.CourierNumber, b.ClientNumber, b.Cheque));
        return Ok(response);
    }

    /// <summary>
    /// Возвращает список истории заказов клиента
    /// </summary>
    /// <returns>Возвращает список истории заказов клиента</returns>
    [HttpGet("last")]
    public async Task<ActionResult<List<ClientGetLast>>> GetHistory()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Запрос клиента на получение истории заказ. User id = {userId}, role = {role}",
            userId, role);

        var orders = await _orderApplicationService.GetHistoryOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new ClientGetLast(b.Id, b.Basket, b.Price, b.Comment,
                b.OrderDate, b.DeliveryDate, b.Cheque));
        return Ok(response);
    }

    /// <summary>
    /// Возвращает список отмененных заказов клинета
    /// </summary>
    /// <returns>Возвращает список отмененных заказов клиента </returns>
    [HttpGet("canceled")]
    public async Task<ActionResult<List<ClientGetCanceled>>> GetCanceled()
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Запрос клиента на получение отмененных заказ. User id = {userId}, role = {role}",
            userId, role);

        var orders = await _orderApplicationService.GetCanceledOrders(role, userId);
        if (orders.Count == 0)
            return NoContent();
        var response = orders.Select(b =>
            new ClientGetCanceled(b.Id, b.Basket, b.Price, b.Comment, b.OrderDate,
                b.CanceledDate, b.Cheque, b.LastStatus, b.ReasonOfCanceled, b.WhoCanceled));
        return Ok(response);
    }

    /// <summary>
    /// Изменяет статус заказа на "Принят", завершает заказ.
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <returns>Статус-код 200 (Ok).</returns>
    [HttpPut("accept/{orderId:Guid}")]
    public async Task<ActionResult> ChangeStatusAccepted(Guid orderId)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Запрос клиента на завершение заказа. User id = {userId}, role = {role}, orderId = {orderId}",
            userId, role, orderId);

        await _orderApplicationService.ChangeStatusCompleted(role, userId, orderId);
        return Ok();
    }
    
    /// <summary>
    /// Изменяет статус заказа на "Отменен".
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <param name="cancelOrderRequest">Запрос на отмену заказа.</param>
    /// <returns>Статус-код 200 (Ok).</returns>
    [HttpPut("cancel/{orderId:Guid}")]
    public async Task<ActionResult> ChangeStatusCanceled(Guid orderId, [FromBody] CancelOrderRequest cancelOrderRequest)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        _logger.LogInformation(
            "Запрос клиента на отмену заказа. User id = {userId}, role = {role}, orderId = {orderId}, reasonOfCanceled = {reasonOfCanceled}",
            userId, role, orderId, cancelOrderRequest.ReasonOfCanceled);

        await _orderApplicationService.ChangeStatusCanceled(role, userId, orderId, cancelOrderRequest.ReasonOfCanceled);
        return Ok();
    }
}