using Handler.Core.HanlderService;

namespace Handler.Core.Abstractions.Repositories;

public interface IPaymentRepository
{
    string? Save(PaymentOrder? order);
    PaymentOrder? Get(Guid orderId);

    string? Delete(Guid orderId);
}