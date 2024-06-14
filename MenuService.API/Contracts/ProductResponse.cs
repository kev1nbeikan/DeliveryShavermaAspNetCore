namespace MenuService.API.Contracts;

public record ProductResponse(
	Guid Id,
	string Title,
	string Description,
	string Composition,
	int Price,
	string ImagePath);