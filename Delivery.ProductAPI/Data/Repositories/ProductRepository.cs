using Delivery.ProductAPI.Abstractions;
using Delivery.ProductAPI.Domain;
using Delivery.ProductAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Delivery.ProductAPI.Data.Repositories;

public class ProductRepository : IProductRepository
{
	private readonly ProductDbContext _dbContext;

	public ProductRepository(ProductDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<List<Product>> Get()
	{
		var productEntities = await _dbContext.Products
			.AsNoTracking()
			.ToListAsync();

		var products = productEntities
			.Select(p => Product.Create(p.Id, p.Title, p.Description, p.Composition, p.Price, p.ImagePath).product)
			.ToList();

		return products;
	}

	public async Task<Guid> Create(Product product)
	{
		var productEntity = new ProductEntity
		{
			Id = product.Id,
			Title = product.Title,
			Description = product.Description,
			Composition = product.Composition,
			Price = product.Price,
			ImagePath = product.ImagePath
		};

		await _dbContext.Products.AddAsync(productEntity);
		await _dbContext.SaveChangesAsync();

		return productEntity.Id;
	}

	public async Task<Guid> Update(
		Guid id,
		string title,
		string description,
		string composition,
		int price,
		string imagePath)
	{
		await _dbContext.Products
			.Where(p => p.Id == id)
			.ExecuteUpdateAsync(
				x => x
					.SetProperty(p => p.Title, p => title)
					.SetProperty(p => p.Description, p => description)
					.SetProperty(p => p.Composition, p => composition)
					.SetProperty(p => p.Price, p => price)
					.SetProperty(p => p.ImagePath, p => imagePath)
			);

		return id;
	}

	public async Task<Guid> Delete(Guid id)
	{
		await _dbContext.Products
			.Where(p => p.Id == id)
			.ExecuteDeleteAsync();

		return id;
	}
}