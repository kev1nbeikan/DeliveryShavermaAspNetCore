using Microsoft.AspNetCore.Mvc;
using StoreService.Core.Abstractions;
using StoreService.Main.Extensions;
using StoreService.Main.Views.Store;

namespace StoreService.Main.Controllers;

[Route("store/[action]")]
public class StoreViewController : Controller
{
    private readonly IStoreService _storeService;
    private readonly ILogger _logger;

    public StoreViewController(IStoreService storeService, ILogger logger)
    {
        _storeService = storeService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var store = await _storeService.GetOrAddNewStore(User.UserId());
            return View(new IndexView
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