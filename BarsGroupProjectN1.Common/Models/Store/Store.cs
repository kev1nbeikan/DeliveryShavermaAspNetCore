using Microsoft.Extensions.Options;

namespace BarsGroupProjectN1.Core.Models.Store;

public class Store()
{
    public Guid Id { get; init; }
    public StoreStatus Status { get; set; }
    public int ActiveOrdersCount { get; set; } = 0;

    public string? Address { get; set; }

    public static Store Create(Guid id, StoreStatus status = StoreStatus.Closed, int activeOrdersCount = 0)
    {
        var store = new Store
        {
            Id = id,
            Status = status,
            ActiveOrdersCount = activeOrdersCount
        };

        store.EnsureIsValid();

        return store;
    }

    public void EnsureIsValid()
    {
        if (Id == Guid.Empty) throw new ArgumentException("Айди магазина не может быть пустым");
        if (ActiveOrdersCount < 0) throw new ArgumentException("Количество активных заказов не может быть меньше нуля");
    }

    public void EnsureIsValidToOpen()
    {
        EnsureIsValid();
        if (string.IsNullOrEmpty(Address)) throw new ArgumentException("Адрес магазина не может быть пустым");
    }
}

public enum StoreStatus
{
    Open,
    Closed
}