
using Handler.Core.Payment;

namespace BarsGroupProjectN1.Core.Contracts;

public record GetCookingTimeRequest(
    string clientAddress,
    List<BucketItem> basket);