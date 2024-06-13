
namespace HandlerService.Contracts;

public record PaymentRequest(List<long> ProductIds, string Comment, string Address, String StoreId);