namespace OrderService.Domain.Exceptions;

/// <summary>
/// Исключение, возникающее при ошибке использования репозитория заказов.
/// </summary>
/// <param name="message">Сообщение об ошибке.</param>
public class FailToUseOrderRepository(string message) : ArgumentException(message);