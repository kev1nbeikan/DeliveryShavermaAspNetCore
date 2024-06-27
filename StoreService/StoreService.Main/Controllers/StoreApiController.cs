using BarsGroupProjectN1.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using StoreService.Core;
using StoreService.Core.Abstractions;
using StoreService.Main.Extensions;

namespace StoreService.Main.Controllers;

[ApiController]
[Route("store/api/v1.0")]
public class StoreApiController : ControllerBase
{
    private readonly ILogger<StoreApiController> _logger;
    private readonly IStoreService _storeService;

    public StoreApiController(ILogger<StoreApiController> logger, IStoreService storeService)
    {
        _logger = logger;
        _storeService = storeService;
    }


    [HttpPost("{storeId}")]
    public async Task<IActionResult> AddNewStore(Guid storeId)
    {
        try
        {
            var store = await _storeService.GetOrAddNewStore(User.UserId());
            return Ok(store);
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }


    [HttpGet("cookingtime")]
    public async Task<IActionResult> GetStore(GetCookingTimeRequest request)
    {
        try
        {
            var cookingTime = await _storeService.GetCookingTime(request.ClientAddress, request.Basket);
            return Ok(cookingTime);
        }
        catch (StoreServiceException e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetStatus()
    {
        try
        {
            var storeId = User.UserId();
            return Ok(await _storeService.GetStatus(storeId));
        }
        catch (StoreServiceException e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("status")]
    public async Task<IActionResult> SetStatus([FromForm] StoreStatus status)
    {
        try
        {
            var storeId = User.UserId();
            await _storeService.UpdateStatus(storeId, status);
            return Ok(await _storeService.GetStatus(storeId));
        }
        catch (StoreServiceException e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }
}