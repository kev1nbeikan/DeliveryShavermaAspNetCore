namespace StoreService.Core.Exceptions;

public abstract class StoreServiceException(string message) : Exception(message);