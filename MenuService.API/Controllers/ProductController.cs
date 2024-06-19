using MenuService.API.Contracts;
using MenuService.API.Models;
using MenuService.Core.Abstractions;
using MenuService.Core.Models;
using MenuService.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace MenuService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
	private readonly IProductService _productService;

	private readonly IWebHostEnvironment _hostEnvironment;

	public ProductController(
		IProductService productService,
		ProductDbContext dbContext,
		IWebHostEnvironment hostEnvironment)
	{
		_productService = productService;
		_hostEnvironment = hostEnvironment;
	}

	[HttpGet]
	public async Task<IActionResult> GetProducts()
	{
		var products = await _productService.GetAllProducts();

		var response = products.Select(
			p => new ProductResponse(
				p.Id,
				p.Title,
				p.Description,
				p.Composition,
				p.Price,
				p.ImagePath
			)
		);

		// return Ok(response); 
		return View(new ProductListViewModel(response));
	}

	[HttpPost]
	public async Task<IActionResult> CreateProduct([FromForm] ProductRequest request)
	{
		if (request.File == null || request.File.Length == 0)
		{
			return BadRequest("File is not selected.");
		}

		var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
		var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(request.File.FileName);
		var filePath = Path.Combine(uploadsFolder, uniqueFileName);

		Directory.CreateDirectory(uploadsFolder);

		await using (var stream = new FileStream(filePath, FileMode.Create))
		{
			await request.File.CopyToAsync(stream);
		}

		var (product, error) = Product.Create(
			Guid.NewGuid(),
			request.Title,
			request.Description,
			request.Composition,
			request.Price,
			uniqueFileName
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
		if (request.File == null || request.File.Length == 0)
		{
			return BadRequest("File is not selected.");
		}

		var products = await _productService.GetAllProducts();

		var existingProduct = products.Where(p => p.Id == id);

		var uniqueFileName = existingProduct.FirstOrDefault().ImagePath;

		if (request.File != null && request.File.Length > 0)
		{
			var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
			uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(request.File.FileName);
			var filePath = Path.Combine(uploadsFolder, uniqueFileName);

			Directory.CreateDirectory(uploadsFolder);

			await using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await request.File.CopyToAsync(stream);
			}

			var oldFilePath = Path.Combine(
				_hostEnvironment.WebRootPath,
				"uploads",
				existingProduct.FirstOrDefault().ImagePath
			);

			if (System.IO.File.Exists(oldFilePath))
			{
				System.IO.File.Delete(oldFilePath);
			}
		}

		await _productService.UpdateProduct(
			id,
			request.Title,
			request.Description,
			request.Composition,
			request.Price,
			uniqueFileName
		);

		return RedirectToAction(nameof(AdminStorePanel));
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> DeleteProduct(Guid id)
	{
		var products = await _productService.GetAllProducts();

		var existingProduct = products.Where(p => p.Id == id);

		var uniqueFileName = Path.Combine(
			_hostEnvironment.WebRootPath,
			"uploads",
			existingProduct.FirstOrDefault().ImagePath
		);

		if (System.IO.File.Exists(uniqueFileName))
		{
			System.IO.File.Delete(uniqueFileName);
		}

		await _productService.DeleteProduct(id);

		// return Ok(id);
		return RedirectToAction(nameof(AdminStorePanel));
	}

	[HttpGet("adminstorepanel")]
	public async Task<IActionResult> AdminStorePanel()
	{
		return await GetProducts();
	}

	[HttpGet("getproductsbyid")]
	public async Task<IActionResult> GetProductsById([FromQuery] ProductListRequest request)
	{
		var products = await _productService.GetAllProducts();

		var response = products.Where(p => request.Guids.Contains(p.Id)).ToList();

		return Ok(response);
	}
}