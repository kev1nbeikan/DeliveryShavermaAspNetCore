using Microsoft.AspNetCore.Mvc;
using StoreService.Core.Abstractions;
using StoreService.Main.Extensions;
using StoreService.Main.Models;

namespace StoreService.Main.Controllers;

[Route("store/[action]")]
public class StoreViewController : Controller
{
    private readonly IStoreService _storeService;
    private readonly IStoreProductsService _storeProductsService;
    private readonly ILogger<StoreApiController> _logger;

    public StoreViewController(IStoreService storeService, ILogger<StoreApiController> logger,
        IStoreProductsService storeProductsService)
    {
        _storeService = storeService;
        _logger = logger;
        _storeProductsService = storeProductsService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var store = await _storeService.GetOrAddNewStore(User.UserId());
            return View(new IndexViewModel
            {
                Store = store
            });
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }


    public async Task<IActionResult> StoreProductsInventory()
    {
        try
        {
            return View(new MenuProductsInventoryViewModel
            {
                productsInventory = await _storeProductsService.GetAll(User.UserId())
            });
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }
}