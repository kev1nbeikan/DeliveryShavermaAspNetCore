using Handler.Core.Common;
using Handler.Core.HanlderService;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions.UseCases;

public interface IPaymentUseCases
{
    public Task<(Product[] products, int price, TemporyOrder? paymentOrder)> ExecutePaymentBuild(
        List<BucketItem> productIdsAndQuantity,
        string comment,
        string address,
        string phoneNumber,
        Guid userId);
}