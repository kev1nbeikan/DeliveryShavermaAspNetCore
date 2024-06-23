using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Contracts.Order;
using OrderService.Api.Extensions;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Models;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("orders")]
public class OrderController(IOrderApplicationService orderApplicationService) : ControllerBase
{
    private readonly IOrderApplicationService _orderApplicationService = orderApplicationService;
    [HttpGet("{orderId:Guid}")]
    public async Task<ActionResult<int>> GetStatus(Guid orderId)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var status = await _orderApplicationService.GetStatus(role, userId, orderId);
        return Ok((int)status);
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder([FromBody]OrderCreateRequest request)
    {
        // var userId = User.UserId();
        // var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        var (order, errorString) = CurrentOrder.Create(
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
            Domain.Models.Code.StatusCode.Cooking);

        if (!string.IsNullOrEmpty(errorString))
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Error when creating an order",
                Detail = errorString,
            };
            return BadRequest(problemDetails);
        }
        await _orderApplicationService.CreateCurrentOrder(order);
        return Ok();
    }
}




