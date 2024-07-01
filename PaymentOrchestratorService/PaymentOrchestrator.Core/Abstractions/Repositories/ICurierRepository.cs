using Handler.Core.Common;

namespace Handler.Core.Abstractions.Repositories;

public interface ICurierRepository
{
    Task<(Curier?, TimeSpan deliveryTime)> GetCourier(string clientAddress);
}