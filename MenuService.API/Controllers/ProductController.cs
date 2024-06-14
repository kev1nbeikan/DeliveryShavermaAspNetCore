using MenuService.API.Contracts;
using MenuService.API.Models;
using MenuService.Core.Abstractions;
using MenuService.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MenuService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
	private readonly IProductService _productService;

	public ProductController(IProductService productService)
	{
		_productService = productService;
	}

	[HttpGet]
	public async Task<IActionResult> GetProducts()
	{
		var products = await _productService.GetAllProducts();

		var response = products.Select(
			p => new ProductResponse(p.Id, p.Title, p.Description, p.Composition, p.Price, p.ImagePath)
		);

		// return Ok(response);
		return View(new ProductListViewModel(products));
	}

	[HttpPost]
	public async Task<IActionResult> CreateProduct([FromForm] ProductRequest request)
	{
		var (product, error) = Product.Create(
			Guid.NewGuid(),
			request.Title,
			request.Description,
			request.Composition,
			request.Price,
			request.ImagePath
		);

		if (!string.IsNullOrEmpty(error))
		{
			return BadRequest(error);
		}

		var productId = await _productService.CreateProduct(product);

		// return Ok(productId);
		return RedirectToAction(nameof(AdminStorePanel));
	}

	[HttpPost("{id:guid}")]
	public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] ProductRequest request)
	{
		var productId = await _productService.UpdateProduct(
			id,
			request.Title,
			request.Description,
			request.Composition,
			request.Price,
			request.ImagePath
		);

		// return Ok(productId);

		return RedirectToAction(nameof(AdminStorePanel));
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> DeleteProduct(Guid id)
	{
		await _productService.DeleteProduct(id);

		// return Ok(id);

		return RedirectToAction(nameof(AdminStorePanel));
	}

	[HttpGet("adminstorepanel")]
	public async Task<IActionResult> AdminStorePanel()
	{
		var products = await _productService.GetAllProducts();

		return View(new ProductListViewModel(products));
	}
}