namespace Handler.Core.Abstractions.Repositories;

public interface IOrderRepository
{
    Task<string?> Save(Order order);
}