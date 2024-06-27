using Handler.Core.Common;

namespace Handler.Core.Abstractions.Services;

public interface ICurierService
{
    Task<(Curier?, TimeSpan deliveryTime)> GetCurier(string clientAddress);
}