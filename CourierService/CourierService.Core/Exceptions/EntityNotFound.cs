using BarsGroupProjectN1.Core.Exceptions;

namespace CourierService.Core.Exceptions;

public class EntityNotFound(string message) : Exception(message)
{
}