using StoreService.Core.Abstractions;

namespace StoreService.Core.Exceptions;

public class StoreEntityException(string message)
    : StoreServiceException("Ошибка при работе с данными магазина: " + message)
{
}