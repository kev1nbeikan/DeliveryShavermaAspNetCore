using MenuService.Core.Models;

namespace MenuService.API.Models;

public class ProductListViewModel
{
	public List<Product> Products { get; }

	public ProductListViewModel(List<Product> products)
	{
		Products = products;
	}
}