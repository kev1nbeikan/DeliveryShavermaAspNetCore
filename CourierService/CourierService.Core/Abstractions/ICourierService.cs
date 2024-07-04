using CourierService.Core.Models;
using CourierService.Core.Models.Code;

namespace CourierService.Core.Abstractions;

public interface ICourierService
{
	Task<Guid> CreateCourier(Courier courier);

	Task<Guid> DeleteCourier(Guid id);

	Task<List<Courier>> GetAllCouriers();
	
	Task<Guid> UpdateCourier(Guid id, CourierStatusCode status);
	
	Task<Courier> GetCourierById(Guid id);
}