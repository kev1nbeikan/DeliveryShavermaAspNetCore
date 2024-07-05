using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Courier;
using Handler.Core.Common;

namespace Handler.Core.Abstractions.Repositories;

public interface ICurierRepository
{
    Task<(OrderTaskExecution<Courier>?, string? error)> FindCourier(string clientAddress, string storeAddress);
}