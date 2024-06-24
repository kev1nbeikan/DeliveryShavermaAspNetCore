namespace StoreService.Core.Abstractions;

public abstract class StoreServiceException(string message) : Exception(message);