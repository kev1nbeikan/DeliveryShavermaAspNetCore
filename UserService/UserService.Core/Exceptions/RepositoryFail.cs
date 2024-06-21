namespace UserService.Core.Exceptions;

public class FailToUpdateRepositoryException<T>(string message, T entity) : Exception(message)
{
    public T Entity => entity;
}