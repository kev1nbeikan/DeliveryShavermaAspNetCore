using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Common;

namespace HandlerService.Application.Services;

public class CurierService : ICurierService
{
    private readonly ICurierRepository _curierRepository;

    public CurierService(ICurierRepository curierRepository)
    {
        _curierRepository = curierRepository;
    }

    public async Task<(Curier?, TimeSpan deliveryTime)> GetCurier(string clientAddress)
    {
        return await _curierRepository.GetCourier(clientAddress);
    }
}