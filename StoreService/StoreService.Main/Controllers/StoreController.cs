using Microsoft.AspNetCore.Mvc;
using StoreService.Core;

namespace StoreService.Main.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoreController : ControllerBase
{
    private readonly ILogger<StoreController> _logger;
    private readonly IStoreService _storeService;

    public StoreController(ILogger<StoreController> logger, IStoreService storeService)
    {
        _logger = logger;
        _storeService = storeService;
    }


    [HttpGet("cookingtime/{storeId}")]
    public async Task<IActionResult> Check(Guid storeId, List<ProductInventory> products)
    {
        TimeSpan cookingTime = await _storeService.GetCookingTime(storeId, products);
        return Ok(cookingTime);
    }
}