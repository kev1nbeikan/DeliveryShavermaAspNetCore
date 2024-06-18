using CourierService.Core.Abstractions;
using CourierService.Core.Models;

namespace CourierService.Application.Services;

public class CourierService : ICourierService
{
	private readonly ICourierRepository _courierRepository;

	public CourierService(ICourierRepository courierRepository)
	{
		_courierRepository = courierRepository;
	}

	public async Task<List<Courier>> GetAllCouriers()
	{
		return await _courierRepository.Get();
	}

	public async Task<Guid> CreateCourier(Courier courier)
	{
		return await _courierRepository.Create(courier);
	}

	public async Task<Guid> UpdateCourier(Guid id, string email, string password)
	{
		return await _courierRepository.Update(id, email, password);
	}

	public async Task<Guid> DeleteCourier(Guid id)
	{
		return await _courierRepository.Delete(id);
	}
}