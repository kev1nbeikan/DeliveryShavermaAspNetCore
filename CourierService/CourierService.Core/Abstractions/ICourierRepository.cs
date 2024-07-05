using BarsGroupProjectN1.Core.Models.Courier;
using CourierService.Core.Models;
using CourierService.Core.Models.Code;

namespace CourierService.Core.Abstractions;

public interface ICourierRepository
{
    Task<Guid> Create(Courier courier);

    Task<Guid> Delete(Guid id);

    Task<List<Courier>> Get();

    Task<Guid> Update(Guid id, CourierStatusCode status);

    Task<bool> Update(Guid id, string phoneNumber);

    Task<Courier?> GetById(Guid id);
}