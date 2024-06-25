using StoreService.Core.Abstractions;

namespace StoreService.Core.Exceptions;

public class StoreNotFoundException(Guid storeId) : StoreServiceException($"Store with ID {storeId} not found.");