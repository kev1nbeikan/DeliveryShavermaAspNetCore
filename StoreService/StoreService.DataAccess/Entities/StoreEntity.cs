using StoreService.Core;

namespace StoreService.DataAccess.Entities;

public class StoreEntity
{
    public Guid Id { get; set; }
    public StoreStatus Status { get; set; }
}