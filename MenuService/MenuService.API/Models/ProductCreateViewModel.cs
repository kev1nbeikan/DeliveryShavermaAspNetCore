namespace MenuService.API.Models;

public class ProductCreateViewModel
{
	public string Title { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public string Composition { get; set; } = string.Empty;

	public int Price { get; set; }

	public IFormFile File { get; set; }
}