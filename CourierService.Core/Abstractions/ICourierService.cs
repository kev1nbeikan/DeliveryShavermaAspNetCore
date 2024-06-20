using CourierService.Core.Models;

namespace CourierService.Core.Abstractions;

public interface ICourierService
{
	Task<Guid> CreateCourier(Courier courier);

	Task<Guid> DeleteCourier(Guid id);

	Task<List<Courier>> GetAllCouriers();

	Task<Guid> UpdateCourier(Guid id, string email, string password);

	Task<Guid> UpdateCourier(Guid id, bool status);
}