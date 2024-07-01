namespace Handler.Core.Exceptions;

public class PaymentBuildException(string message) : Exception($"Ошибка при формировании платежа: {message}");