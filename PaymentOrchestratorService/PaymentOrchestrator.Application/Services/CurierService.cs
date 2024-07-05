using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Courier;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Common;

namespace HandlerService.Application.Services;

public class CurierService : ICurierService
{
    private readonly ICurierRepository _courierRepository;

    public CurierService(ICurierRepository courierRepository)
    {
        _courierRepository = courierRepository;
    }

    public async Task<(OrderTaskExecution<Courier>? courier, string? error)> FindCourier(string clientAddress,
        string storeAddress)
    {
        return await _courierRepository.FindCourier(clientAddress, storeAddress);
    }
}