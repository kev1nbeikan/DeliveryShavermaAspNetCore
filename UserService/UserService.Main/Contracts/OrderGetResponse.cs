using UserService.Core;

namespace UserService.Main.Contracts;

public record OrderGetResponse(
    Guid Id,
    StatusCode Status,
    List<BasketItem> Basket,
    int Price,
    string Comment,
    string ClientAddress,
    string CourierNumber,
    string ClientNumber,
    string Cheque);