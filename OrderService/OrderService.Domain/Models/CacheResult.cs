using OrderService.Domain.Exceptions;

namespace OrderService.Domain.Models;

public class CacheResult<T>
{
    public T? Value { get; set; }
    public bool IsFound { get; set; }

    public static CacheResult<T> Found(T value)
    {
        return new CacheResult<T>
        {
            Value = value,
            IsFound = true
        };
    }

    public static CacheResult<T> NotFound()
    {
        return new CacheResult<T>
        {
            Value = default,
            IsFound = false
        };
    }
}