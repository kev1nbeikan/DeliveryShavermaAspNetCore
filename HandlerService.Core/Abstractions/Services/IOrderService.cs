namespace Handler.Core.Abstractions.Services;

public interface IOrderService
{
    Task<string?> Save(Order order);
}