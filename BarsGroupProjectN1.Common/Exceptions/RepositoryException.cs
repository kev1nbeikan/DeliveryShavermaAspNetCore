namespace BarsGroupProjectN1.Core.Exceptions;

public class RepositoryException(string message) : Exception($"Ошибка доступа к данным: {message}.")
{
}