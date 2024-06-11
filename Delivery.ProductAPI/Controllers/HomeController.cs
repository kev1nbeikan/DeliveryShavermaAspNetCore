using Delivery.ProductAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Delivery.ProductAPI.Models;

namespace Delivery.ProductAPI.Controllers;

public class HomeController : Controller
{
	private readonly DeliveryAppDbContext _dbContext;

	public HomeController(DeliveryAppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public IActionResult Index()
	{
		var products = _dbContext.Products.ToList();

		return View(new ProductListViewModel(products));
	}
}