using Handler.Core.Common;
using HandlerService.Controllers;

namespace Handler.Core.Abstractions.Services;

public interface ICurierService
{
    Task<(Curier?, TimeSpan deliveryTime)> GetCurier(string clientAddress);
}