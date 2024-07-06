using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Exceptions;
using BarsGroupProjectN1.Core.Models.Courier;
using CourierService.API.Contracts;
using CourierService.API.Extensions;
using CourierService.API.Models;
using CourierService.Core.Abstractions;
using CourierService.Core.Exceptions;
using CourierService.Core.Models;
using CourierService.Core.Models.Code;
using Microsoft.AspNetCore.Mvc;

namespace CourierService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourierController : Controller
{
    private readonly ICourierService _courierService;

    private readonly IOrdersApiClient _ordersApiClient;

    private readonly ILogger<CourierController> _logger;

    public CourierController(
        ICourierService courierService,
        IOrdersApiClient ordersApiClient,
        ILogger<CourierController> logger
    )
    {
        _courierService = courierService;
        _ordersApiClient = ordersApiClient;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetCouriers()
    {
        var couriers = await _courierService.GetAllCouriers();

        var response = couriers.Select(c => new CourierResponse(c.Id, c.Status));

        return Ok(response);
    }

    [HttpGet("getcourierbyid")]
    public async Task<IActionResult> GetCourierById(Guid id)
    {
        var courier = await _courierService.GetCourierById(id);

        return Ok(courier);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourier([FromBody] CourierRequest request)
    {
        var (courier, error) = Courier.Create(
            Guid.NewGuid(),
            status: default
        );

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        await _courierService.CreateCourier(courier);

        return Ok(courier);
    }

    [HttpPost("status/{id:guid}")]
    public async Task<IActionResult> UpdateCourierStatus(Guid id, [FromForm] CourierStatusCode status)
    {
        try
        {
            await _courierService.UpdateCourier(id, status);
        }
        catch (RepositoryException e)
        {
            _logger.LogError(e, "Error updating courier status");
            return BadRequest(e.Message);
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
        catch (EntityNotFound e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }

        return RedirectToAction(nameof(CourierProfile));
    }


    [HttpPut("update/{id:guid}")]
    public async Task<IActionResult> UpdateCourier(Guid id, [FromForm] CourierEditRequest request)
    {
        try
        {
            await _courierService.UpdateCourier(id, request.PhoneNumber);
            return Ok();
        }
        catch (RepositoryException e)
        {
            _logger.LogError(e, "Error updating courier status");
            return BadRequest(e.Message);
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
        catch (EntityNotFound e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCourier(Guid id)
    {
        return Ok(await _courierService.DeleteCourier(id));
    }

    [HttpGet("edit")]
    public async Task<IActionResult> CourierEdit()
    {
        var courierId = User.UserId();

        try
        {
            var courier = await _courierService.GetCourierById(courierId);

            return View(new CourierEditViewModel()
            {
                Courier = courier
            });
        }
        catch (EntityNotFound e)
        {
            _logger.LogError(e, e.Message);

            return BadRequest(e.Message);
        }
    }


    [HttpGet("CurrentOrder")]
    public IActionResult CurrentOrder()
    {
        return View();
    }


    [HttpGet("LastOrder")]
    public IActionResult LastOrder()
    {
        return View();
    }

    [HttpGet("CancelOrder")]
    public IActionResult CancelOrder()
    {
        return View();
    }


    [HttpGet("profile")]
    public async Task<IActionResult> CourierProfile()
    {
        var courierId = User.UserId();

        try
        {
            var courier = await _courierService.GetCourierById(courierId);

            return View(new CourierViewModel
            {
                Id = courier.Id,
                Status = courier.Status,
                ActiveOrdersCount = courier.ActiveOrdersCount
            });
        }

        catch (EntityNotFound e)
        {
            var (newCourier, error) = Courier.Create(
                courierId,
                status: default
            );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            await _courierService.CreateCourier(newCourier);

            return RedirectToAction("CourierProfile", new { id = newCourier.Id });
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
        catch (RepositoryException e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("find")]
    public async Task<IActionResult> GetActiveCourier([FromQuery] string clientAddress, string storeAddress)
    {
        var couriers = await _courierService.GetAllCouriers();

        var activeCourier = couriers.FirstOrDefault(c => c.Status == CourierStatusCode.Active);

        if (activeCourier is null)
        {
            return NotFound("Courier not found");
        }

        return Ok(
            new OrderTaskExecution<Courier>
            {
                Executor = activeCourier,
                Time = TimeSpan.FromMinutes(15)
            }
        );
    }
}