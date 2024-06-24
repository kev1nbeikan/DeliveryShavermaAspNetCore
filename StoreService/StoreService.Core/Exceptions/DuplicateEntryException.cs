namespace StoreService.Core.Exceptions;

public class DuplicateEntryException<T>(string message, T entry) : Exception(message);