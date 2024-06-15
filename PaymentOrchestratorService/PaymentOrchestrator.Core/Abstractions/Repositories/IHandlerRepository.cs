using Handler.Core.HanlderService;

namespace Handler.Core.Abstractions.Repositories;

public interface IHandlerRepository
{
    string? Save(TemporyOrder? order);
    TemporyOrder? Get(Guid orderId);

    string? Delete(Guid orderId);
}