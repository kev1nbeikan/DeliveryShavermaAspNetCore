
using BarsGroupProjectN1.Core.Models.Payment;

namespace BarsGroupProjectN1.Core.Contracts;

public record GetCookingTimeRequest(
    string ClientAddress,
    List<ProductsInventory> Basket);