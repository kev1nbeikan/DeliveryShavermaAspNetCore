namespace OrderService.Domain.Exceptions;

/// <summary>
/// Исключение, возникающее при ошибке создания модели заказа.
/// </summary>
/// <param name="message">Сообщение об ошибке.</param>
public class FailToCreateOrderModel(string message): ArgumentException(message);