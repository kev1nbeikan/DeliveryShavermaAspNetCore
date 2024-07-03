using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;

namespace BarsGroupProjectN1.Core.Contracts.Orders;

public record OrderCreateRequest(
    Guid Id,
    Guid ClientId,
    Guid CourierId,
    Guid StoreId,
    List<BasketItem> Basket,
    int Price,
    string Comment,
    string StoreAddress,
    string ClientAddress,
    string CourierNumber,
    string ClientNumber,
    TimeSpan CookingTime,
    TimeSpan DeliveryTime,
    string Cheque);