using CourierService.Core.Models;

namespace CourierService.Core.Abstractions;

public interface ICourierRepository
{
	Task<Guid> Create(Courier courier);

	Task<Guid> Delete(Guid id);

	Task<List<Courier>> Get();

	Task<Guid> Update(Guid id, string email, string password);
}