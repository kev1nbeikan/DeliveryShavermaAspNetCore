using Delivery.ProductAPI.Domain;

namespace Delivery.ProductAPI.Abstractions;

public interface IProductRepository
{
	Task<Guid> Create(Product product);

	Task<Guid> Delete(Guid id);

	Task<List<Product>> Get();

	Task<Guid> Update(
		Guid id,
		string title,
		string description,
		string composition,
		int price,
		string imagePath);
}