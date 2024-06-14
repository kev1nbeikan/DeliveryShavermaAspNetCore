using Microsoft.Extensions.Caching.Memory;

namespace HandlerService.UnitTests;

public static class TestFixture
{
    private static IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    public static IMemoryCache InMemory()
    {
        return _cache;
    }
}