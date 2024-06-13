namespace Delivery.ProductAPI.Contracts;

public record ProductRequest(
	string title,
	string description,
	string composition,
	int price,
	string imagePath);