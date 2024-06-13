namespace Handler.Core.Abstractions;

public interface IMenuService
{
    (Product[] products, string? error) GetProducts(List<long> productIds);
}