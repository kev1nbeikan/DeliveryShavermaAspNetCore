using Delivery.ProductAPI.Abstractions;
using Delivery.ProductAPI.Domain;

namespace Delivery.ProductAPI.Services;

public class ProductService : IProductService
{
	private readonly IProductRepository _productRepository;

	public ProductService(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task<List<Product>> GetAllProducts()
	{
		return await _productRepository.Get();
	}

	public async Task<Guid> CreateProduct(Product product)
	{
		return await _productRepository.Create(product);
	}

	public async Task<Guid> UpdateProduct(
		Guid id,
		string title,
		string description,
		string composition,
		int price,
		string imagePath)
	{
		return await _productRepository.Update(id, title, description, composition, price, imagePath);
	}

	public async Task<Guid> DeleteProduct(Guid id)
	{
		return await _productRepository.Delete(id);
	}
}