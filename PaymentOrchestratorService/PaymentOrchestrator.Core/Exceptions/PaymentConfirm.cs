namespace Handler.Core.Exceptions;

public class PaymentConfirmException(string message) : Exception($"Ошибка при при формировании заказа: {message}");