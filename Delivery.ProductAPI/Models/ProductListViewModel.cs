using Delivery.ProductAPI.Domain;

namespace Delivery.ProductAPI.Models;

public class ProductListViewModel
{
	public List<Product> Products { get; }

	public ProductListViewModel(List<Product> products)
	{
		Products = products;
	}
}