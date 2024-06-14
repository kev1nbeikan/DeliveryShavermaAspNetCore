using MenuService.API.Contracts;
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

		return Ok(response);
	}

	[HttpPost]
	public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
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

		return Ok(productId);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductRequest request)
	{
		var productId = await _productService.UpdateProduct(
			id,
			request.Title,
			request.Description,
			request.Composition,
			request.Price,
			request.ImagePath
		);

		return Ok(productId);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteProduct(Guid id)
	{
		return Ok(await _productService.DeleteProduct(id));
	}
}