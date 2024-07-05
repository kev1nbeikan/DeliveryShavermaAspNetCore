using CourierService.Core.Models;
using CourierService.Core.Models.Code;

namespace CourierService.Core.Abstractions;

public interface ICourierService
{
    Task<Guid> CreateCourier(Courier courier);

    Task<Guid> DeleteCourier(Guid id);

    Task<List<Courier>> GetAllCouriers();

    /// <summary>
    /// Обновляет статус курьера.
    /// </summary>
    /// <param name="id">Идентификатор курьера для обновления.</param>
    /// <param name="status">Новый статус курьера.</param>
    /// <returns>Идентификатор обновленного курьера.</returns>
    /// <exception cref="ArgumentException">Сгенерировано, если курьер не найден или если номер телефона не указан.</exception>
    Task<Guid> UpdateCourier(Guid id, CourierStatusCode status);

    Task<Courier> GetCourierById(Guid id);
}