using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Payment;
using BarsGroupProjectN1.Core.Models.Store;
using Microsoft.AspNetCore.Mvc;
using StoreService.Core;
using StoreService.Core.Abstractions;
using StoreService.Core.Exceptions;
using StoreService.Main.Extensions;

namespace StoreService.Main.Controllers;

[ApiController]
[Route("store/api/v1.0")]
public class StoreApiController : ControllerBase
{
    private readonly ILogger<StoreApiController> _logger;
    private readonly IStoreService _storeService;
    private readonly IStoreProductsService _storeProductService;

    public StoreApiController(
        ILogger<StoreApiController> logger,
        IStoreService storeService,
        IStoreProductsService storeProductService)
    {
        _logger = logger;
        _storeService = storeService;
        _storeProductService = storeProductService;
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


    [HttpGet("get-cooking-info")]
    public async Task<IActionResult> GetCookingInfo(GetCookingTimeRequest request)
    {
        try
        {
            var cookingInfo = await _storeService.GetCookingInfo(request.ClientAddress, request.Basket);
            return Ok(cookingInfo);
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

    [HttpPost("inventory")]
    public async Task<IActionResult> UpsertStoreProductInventory([FromBody] ProductsInventoryWithoutStore request)
    {
        try
        {
            await _storeProductService.UpsertProductInventory(
                User.UserId(),
                request.ProductId,
                request.Quantity);
            return Ok();
        }
        catch (StoreServiceException e)

        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}