using StoreService.Core.Abstractions;

namespace StoreService.Core.Exceptions;

public class StoreClosedException : StoreServiceException
{
    public StoreClosedException(Guid storeId) 
        : base($"Store with ID {storeId} is closed.")
    {
    }
}