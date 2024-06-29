using StoreService.Core.Abstractions;

namespace StoreService.Core.Exceptions;

public class StoreNotFoundException : StoreServiceException
{
    public StoreNotFoundException(Guid storeId)
        : base($"Магазин с id {storeId} не найден")
    {
    }

    public StoreNotFoundException(string clientAddress)
        : base($"Нет доступного магазина для адреса: {clientAddress}")
    {
    }
    
    
}