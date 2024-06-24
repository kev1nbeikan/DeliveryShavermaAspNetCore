namespace StoreService.Core;

public class Store()
{
    public Guid Id { get; init; }
    public StoreStatus Status { get; init; }

    public static Store Create(Guid id, StoreStatus status)
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