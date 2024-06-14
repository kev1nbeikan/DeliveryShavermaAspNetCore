namespace Handler.Core.Abstractions.Repositories;

public interface IMenuRepository
{
    public Task<(Product[] products, string? error)> Get(List<Guid> productIds);
}