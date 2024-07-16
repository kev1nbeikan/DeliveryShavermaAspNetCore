using BarsGroupProjectN1.Core.Extensions;
using BarsGroupProjectN1.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Core = BarsGroupProjectN1.Core.Models.Order;
using OrderService.Api.Contracts.Order;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Models;

namespace OrderService.Api.Controllers;

/// <summary>
/// Контроллер, отвечающий за обработку запросов связанных с созданием заказа и получением его статуса.
/// </summary>
/// <param name="orderApplicationService">Сервис, предоставляющий возможность работы с заказами.</param>
/// <param name="logger">Сервис, предоставляющий возможность логгирования.</param>
[ApiController]
[Route("orders")]
public class OrderController(IOrderApplicationService orderApplicationService, ILogger<OrderController> logger)
    : ControllerBase
{
    private readonly IOrderApplicationService _orderApplicationService = orderApplicationService;
    private readonly ILogger<OrderController> _logger = logger;

    /// <summary>
    /// Запрос на получение статутса заказа.
    /// </summary>
    /// <param name="orderId">Id заказа у которого нужно получить статус.</param>
    /// <returns>Статус-код 200 (Ok).</returns>
    [HttpGet("status/{orderId:Guid}")]
    public async Task<ActionResult<int>> GetStatus(Guid orderId)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());
        _logger.LogInformation(
            "Запрос на получение статутса заказа. OrderId = {OrderId}, UserId = {UserId}, Role = {Role}", orderId,
            userId, role);
        var status = await _orderApplicationService.GetStatus(role, userId, orderId);
        return Ok((int)status);
    }

    /// <summary>
    /// Запрос на создание заказа
    /// </summary>
    /// <param name="request"> Данные для создания заказа</param>
    /// <returns>Статус-код 200 (Ok).</returns>
    /// <exception cref="System.ArgumentNullException">Вызывается, если param равен null.</exception>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///     {
    ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///         "clientId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///         "courierId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///         "storeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///         "basket": [
    ///             {
    ///                 "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///                 "name": "string",
    ///                 "amount": 0,
    ///                 "price": 0
    ///             }
    ///         ],
    ///         "price": 0,
    ///         "comment": "string",
    ///         "storeAddress": "string",
    ///         "clientAddress": "string",
    ///         "courierNumber": "89874554046",
    ///         "clientNumber": "89874554046",
    ///         "cookingTime": "00:00:01",
    ///         "deliveryTime": "00:00:01",
    ///         "cheque": "string"
    ///     }
    /// </remarks>
    [HttpPost]
    public async Task<ActionResult> CreateOrder([FromBody] OrderCreateRequest request)
    {
        _logger.LogInformation("Запрос на создание заказа. Request = {Request}", request);

        var order = CurrentOrder.Create(
            request.Id,
            request.ClientId,
            request.CourierId,
            request.StoreId,
            request.Basket,
            request.Price,
            request.Comment,
            request.StoreAddress,
            request.ClientAddress,
            request.CourierNumber,
            request.ClientNumber,
            request.CookingTime,
            request.DeliveryTime,
            DateTime.UtcNow,
            null,
            null,
            request.Cheque,
            Core.StatusCode.Cooking);
        await _orderApplicationService.CreateCurrentOrder(order);

        _logger.LogInformation("Создан новый заказ. Order = {Order}", order);
        return Ok();
    }


    [HttpPut("test/{Id:Guid}")]
    public async Task<ActionResult> ChangeId(Guid Id)
    {
        UserClaimsExtensions.IdOfRequest = Id;
        return Ok(UserClaimsExtensions.IdOfRequest);
    }

    [HttpPut("test/{role:int}")]
    public async Task<ActionResult> ChangeStatus(int role)
    {
        UserClaimsExtensions.RoleOfRequest = role.ToString();
        return Ok(UserClaimsExtensions.RoleOfRequest);
    }

    [HttpGet("test/id")]
    public async Task<ActionResult> GetId()
    {
        return Ok(UserClaimsExtensions.IdOfRequest);
    }

    [HttpGet("test/role")]
    public async Task<ActionResult> GetRole()
    {
        return Ok(UserClaimsExtensions.RoleOfRequest);
    }
}