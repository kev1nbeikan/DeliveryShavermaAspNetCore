using System;

namespace StoreService.Core;

public class Store()
{
    public Guid Id { get; init; }
    public StoreStatus Status { get; set; }

    public static Store Create(Guid id, StoreStatus status = StoreStatus.Closed)
    {
        if (id == Guid.Empty) throw new ArgumentException(nameof(id) + " cannot be empty");

        return new Store
        {
            Id = id,
            Status = status
        };
    }
}

public enum StoreStatus
{
    Open,
    Closed
}