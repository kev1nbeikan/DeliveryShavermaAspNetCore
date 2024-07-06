using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Courier;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Common;

namespace HandlerService.Application.Services;

public class CourierService : ICourierService
{
    private readonly ICourierRepository _courierRepository;

    public CourierService(ICourierRepository courierRepository)
    {
        _courierRepository = courierRepository;
    }

    public async Task<(OrderTaskExecution<Courier>? courier, string? error)> GetDeliveryExecution(string clientAddress,
        string storeAddress)
    {
        return await _courierRepository.GetDeliveryExecution(clientAddress, storeAddress);
    }
}