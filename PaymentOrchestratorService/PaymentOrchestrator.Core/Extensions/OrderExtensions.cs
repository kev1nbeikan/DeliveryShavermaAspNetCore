using System.Runtime.CompilerServices;
using Handler.Core.Common;

namespace Handler.Core.Extensions;

public static class OrderExtensions
{
    public static Dictionary<Guid, int>? ToOrderBucket(this Product[] products)
    {
        return products.ToDictionary(
            x => x.Id,
            x => x.Price
        );
    }
}