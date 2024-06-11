using Delivery.ProductAPI.Data;
using Delivery.ProductAPI.Domain;
using Delivery.ProductAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.ProductAPI.Controllers;

public class ProductController : Controller
{
	private readonly DeliveryAppDbContext _dbContext;

	public ProductController(DeliveryAppDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	
	[HttpGet]
	public IActionResult Index()
	{
		var items = _dbContext.Items.ToList();

		return View(new ProductListViewModel(items));
	}

	[HttpPost]
	public async Task<IActionResult> CreateAsync(string title, string description, string composition, int price)
	{
		var product = new Product
		{
			Title = title,
			Description = description,
			Composition = composition,
			Price = price,
			ImagePath = "imagePath"
		};

		_dbContext.Items.Add(product);
		await _dbContext.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
	}
}