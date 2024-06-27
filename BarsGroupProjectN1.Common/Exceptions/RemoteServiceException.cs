namespace BarsGroupProjectN1.Core.Exceptions;

public class RemoteServiceException : RepositoryException

{
    public RemoteServiceException(string serviceName, string url, string suggestions, string errorMessage = "") : base(
        $"Сервис {serviceName} не доступен по адресу {url}. Подробности: {errorMessage} Рекоммендации: {suggestions}")
    {
    }
}