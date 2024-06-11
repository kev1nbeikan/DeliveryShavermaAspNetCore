namespace Delivery.ProductAPI.Models;

public class AddProductViewModel
{
	public string Title { get; set; }
	
	public string Description { get; set; }
	
	public string Composition { get; set; }
	
	public int Price { get; set; }
}