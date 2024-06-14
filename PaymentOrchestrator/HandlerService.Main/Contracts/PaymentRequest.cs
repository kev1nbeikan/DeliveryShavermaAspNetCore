namespace HandlerService.Contracts;

public record PaymentRequest(List<Guid> ProductIds, string Comment, string Address, String StoreId);