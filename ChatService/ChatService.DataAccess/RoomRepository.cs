//
// using Microsoft.Extensions.Caching.Memory;
//
// namespace ChatService.DataAccess;
//
// public class RoomRepository
// {
//     private readonly IMemoryCache _cache;
//     private readonly MemoryCacheEntryOptions _entryMemoryCacheOptions;
//
//
//     public RoomRepository(IMemoryCache cache)
//     {
//         _cache = cache;
//         _entryMemoryCacheOptions = new MemoryCacheEntryOptions();
//     }
//
//     public string? Upsert(string roomHash, Guid userId)
//     {
//         _cache.Set(roomHash, order, _entryMemoryCacheOptions);
//
//         return null;
//     }
//
//     public PaymentOrder? Get(Guid orderId)
//     {
//         return _cache.TryGetValue(orderId.ToString(), out PaymentOrder? order)
//             ? order
//             : null;
//     }
//
//     public string? Delete(Guid orderId)
//     {
//         _cache.Remove(orderId.ToString());
//         return null;
//     }
// }