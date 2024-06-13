namespace Delivery.ProductAPI.Contracts;

public record ProductResponse(
	Guid id,
	string title,
	string description,
	string composition,
	int price,
	string imagePath);