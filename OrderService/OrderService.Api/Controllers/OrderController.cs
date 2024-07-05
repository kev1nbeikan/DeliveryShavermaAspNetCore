using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Core = BarsGroupProjectN1.Core.Models.Order;
using OrderService.Api.Contracts.Order;
using OrderService.Api.Extensions;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Common.Code;
using OrderService.Domain.Models;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("orders")]
public class OrderController(IOrderApplicationService orderApplicationService) : ControllerBase
{
    private readonly IOrderApplicationService _orderApplicationService = orderApplicationService;

    [HttpGet("status/{orderId:Guid}")]
    public async Task<ActionResult<int>> GetStatus(Guid orderId)
    {
        var userId = User.UserId();
        var role = (RoleCode)Enum.Parse(typeof(RoleCode), User.Role());

        try
        {
            var status = await _orderApplicationService.GetStatus(role, userId, orderId);
            return Ok((int)status);
        }
        catch (KeyNotFoundException)
        {
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(); 
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder([FromBody] OrderCreateRequest request)
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
            Core.StatusCode.Cooking);

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
    
    
    [HttpPut ("test/{Id:Guid}")]
    public async Task<ActionResult> ChangeId(Guid Id)
    {
        UserClaimsExtensions.IdOfRequest = Id;
        return Ok(UserClaimsExtensions.IdOfRequest);
    }
    
    [HttpPut ("test/{role:int}")]
    public async Task<ActionResult> ChangeStatus(int role)
    {
        UserClaimsExtensions.RoleOfRequest = role.ToString();
        return Ok(UserClaimsExtensions.RoleOfRequest);
    }
    
    [HttpGet ("test/id")]
    public async Task<ActionResult> GetId()
    {
        return Ok(UserClaimsExtensions.IdOfRequest);
    }
    
    [HttpGet ("test/role")]
    public async Task<ActionResult> GetRole()
    {
        return Ok(UserClaimsExtensions.RoleOfRequest);
    }
}