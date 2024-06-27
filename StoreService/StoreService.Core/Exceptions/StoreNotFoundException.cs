using StoreService.Core.Abstractions;

namespace StoreService.Core.Exceptions;

public class StoreNotFoundException : StoreServiceException
{
    public StoreNotFoundException(Guid storeId)
        : base($"Store with ID {storeId} not found.")
    {
    }

    public StoreNotFoundException(string clientAddress)
        : base($"Нет доступного магазина для адреса: {clientAddress}")
    {
    }
}