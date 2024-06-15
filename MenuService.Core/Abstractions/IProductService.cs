using MenuService.Core.Models;

namespace MenuService.Core.Abstractions;

public interface IProductService
{
	Task<Guid> CreateProduct(Product product);

	Task<Guid> DeleteProduct(Guid id);

	Task<List<Product>> GetAllProducts();

	Task<Guid> UpdateProduct(
		Guid id,
		string title,
		string description,
		string composition,
		int price,
		string imagePath);
}