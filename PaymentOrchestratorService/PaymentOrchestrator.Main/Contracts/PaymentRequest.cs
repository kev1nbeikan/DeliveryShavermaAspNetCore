using Handler.Core.Payment;

namespace HandlerService.Contracts;

public record PaymentRequest(List<BucketItem> ProductIdsAndQuantity, string Comment, string Address, string PhoneNumber, String StoreId);