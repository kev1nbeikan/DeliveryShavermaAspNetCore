using Handler.Core.Payment;

namespace HandlerService.DataAccess.Contracts;

public record GetCookingTimeRequest(
    string clientAddress,
    List<ProductWithAmount> basket);