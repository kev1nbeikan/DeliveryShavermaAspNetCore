using System;

namespace StoreService.Core.Exceptions;

public class NotFoundException<T>(string message, T entity): Exception(message)
{
    
}