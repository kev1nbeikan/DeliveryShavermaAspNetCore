using Handler.Core.Payment;

namespace HandlerService.Contracts;

public record PaymentRequest(List<ProductWithAmount> ProductIdsAndQuantity, string Comment, string Address, string PhoneNumber);