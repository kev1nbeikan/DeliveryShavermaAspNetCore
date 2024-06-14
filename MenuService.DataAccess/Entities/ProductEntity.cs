namespace MenuService.DataAccess.Entities;

public class ProductEntity
{
	public Guid Id { get; set; }

	public string Title { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public string Composition { get; set; } = string.Empty;

	public int Price { get; set; }

	public string ImagePath { get; set; } = string.Empty;
}