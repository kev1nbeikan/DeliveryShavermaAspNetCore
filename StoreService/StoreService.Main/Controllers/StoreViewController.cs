using Microsoft.AspNetCore.Mvc;
using StoreService.Core.Abstractions;
using StoreService.Main.Extensions;
using StoreService.Main.Models;

namespace StoreService.Main.Controllers;

[Route("store/[action]")]
public class StoreViewController : Controller
{
    private readonly IStoreService _storeService;
    private readonly ILogger<StoreApiController> _logger;
    private readonly IProductInventoryMapper _productMapper;

    public StoreViewController(IStoreService storeService,
        ILogger<StoreApiController> logger,
        IProductInventoryMapper productMapper)
    {
        _storeService = storeService;
        _logger = logger;
        _productMapper = productMapper;
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
            return View(new StoreProductsInventoryViewModel()
            {
                ProductsInventory = await _productMapper.GetMappedProducts(User.UserId())
            });
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }
}