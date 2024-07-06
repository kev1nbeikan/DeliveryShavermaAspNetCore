using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Courier;
using Handler.Core.Common;

namespace Handler.Core.Abstractions.Repositories;

public interface ICourierRepository
{
    Task<(OrderTaskExecution<Courier>?, string? error)> GetDeliveryExecution(string clientAddress, string storeAddress);
}