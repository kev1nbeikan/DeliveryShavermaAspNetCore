using Delivery.ProductAPI.Data;
using Delivery.ProductAPI.Domain;
using Delivery.ProductAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delivery.ProductAPI.Controllers;

[Route("/api/products")]
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
		var products = _dbContext.Products.ToList();

		return View(new ProductListViewModel(products));
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

		_dbContext.Products.Add(product);
		await _dbContext.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
	}

	[HttpGet("delete/{id:long}")]
	public async Task<IActionResult> DeleteAsync(long id)
	{
		var product = await _dbContext.Products.FindAsync(id);

		_dbContext.Products.Remove(product!);
		await _dbContext.SaveChangesAsync();

		return RedirectToAction("Index");
	}

	[HttpGet("update/{product}")]
	public async Task<IActionResult> UpdateAsync(Product product)
	{
		await _dbContext.Products.Where(p => p.Id == product.Id).ExecuteUpdateAsync(
			x => x
				.SetProperty(p => p.Title, p => product.Title)
				.SetProperty(p => p.Description, p => product.Description)
				.SetProperty(p => p.Composition, p => product.Composition)
				.SetProperty(p => p.Price, p => product.Price)
		);

		await _dbContext.SaveChangesAsync();

		return RedirectToAction("Index");
	}
}