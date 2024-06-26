using Handler.Core.Common;
using HandlerService.Controllers;

namespace Handler.Core.Abstractions.Repositories;

public interface ICurierRepository
{
    Task<(Curier?, TimeSpan deliveryTime)> GetCurier(string clientAddress);
}