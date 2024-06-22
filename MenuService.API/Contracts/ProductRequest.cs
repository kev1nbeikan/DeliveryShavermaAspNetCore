namespace MenuService.API.Contracts;

public record ProductRequest(
	string Title,
	string Description,
	string Composition,
	int Price,
	IFormFile? File);