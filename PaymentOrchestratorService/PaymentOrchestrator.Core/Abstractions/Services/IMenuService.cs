using Handler.Core.Payment;

namespace Handler.Core.Abstractions.Services;

public interface IMenuService
{
    Task<(Product[] products, string? error)> GetProducts(List<Guid> productIds);
}