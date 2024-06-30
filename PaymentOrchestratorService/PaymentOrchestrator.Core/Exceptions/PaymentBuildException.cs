namespace Handler.Core.Contracts;

public class PaymentBuildException(string message) : Exception($"Ошибка при формировании платежа: {message}");