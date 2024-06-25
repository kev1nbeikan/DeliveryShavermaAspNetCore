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

    public StoreViewController(IStoreService storeService, ILogger<StoreApiController> logger)
    {
        _storeService = storeService;
        _logger = logger;
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
}