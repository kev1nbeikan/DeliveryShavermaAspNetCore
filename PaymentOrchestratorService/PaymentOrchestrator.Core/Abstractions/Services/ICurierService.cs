using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Courier;
using Handler.Core.Common;

namespace Handler.Core.Abstractions.Services;

public interface ICurierService
{
    Task<(OrderTaskExecution<Courier>? courier, string? error)> FindCourier(string clientAddress, string storeAddress);
}