using MenuService.API.Contracts;

namespace MenuService.API.Models;

public class ProductListViewModel
{
	public IEnumerable<ProductResponse> Products { get; }

	public ProductListViewModel(IEnumerable<ProductResponse> products)
	{
		Products = products;
	}
}