using BarsGroupProjectN1.Core.Exceptions;
using BarsGroupProjectN1.Core.Models.Courier;
using BarsGroupProjectN1.Core.Models.Order;
using CourierService.Core.Abstractions;
using CourierService.Core.Exceptions;
using CourierService.Core.Models;
using CourierService.Core.Models.Code;

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

    public async Task<Guid> UpdateCourier(Guid id, CourierStatusCode status)
    {
        var courier = await GetCourierById(id);
        if (string.IsNullOrEmpty(courier.PhoneNumber))
            throw new ArgumentException("Вы не указали номер телефона. Укажите его в профиле чтобы выйти на заказы.");
        if (courier.ActiveOrdersCount > 0)
            throw new ArgumentException(
                "Вы не можете обновить статус курьера, когда он имеет активные заказы. Отмените или закончите их.");

        return await _courierRepository.Update(id, status);
    }

    public async Task<Guid> UpdateCourier(Guid id, string phoneNumber)
    {
        var courier = await GetCourierById(id);

        var courierCreateResult = Courier.Create(courier.Id, courier.Status, courier.ActiveOrdersCount, phoneNumber);

        if (!string.IsNullOrEmpty(courierCreateResult.Error)) throw new ArgumentException(courierCreateResult.Error);

        if (!await _courierRepository.Update(courier.Id, phoneNumber))
            throw new RepositoryException("Не удалось обновить курьера");

        return courierCreateResult.Courier.Id;
    }

    public async Task<Guid> DeleteCourier(Guid id)
    {
        return await _courierRepository.Delete(id);
    }

    public async Task<Courier> GetCourierById(Guid id)
    {
        var courier = await _courierRepository.GetById(id);
        if (courier is null)
            throw new EntityNotFound("Курьер не найден. Попробуйте войти заново или обновить страницу профиля");
        return courier;
    }

    public async Task OnOrderCreate(PublishOrder order)
    {
        var courier = await GetCourierById(order.CourierId);
        if (courier.Status != CourierStatusCode.Active)
            throw new EntityNotFound("Активного курьера с таким идентификатором не существует");

        await _courierRepository.AdjustActiveOrdersCount(courier.Id, adjustment: 1);
    }

    public async Task OnOrderUpdate(PublishOrder order)
    {
        var courier = await GetCourierById(order.CourierId);
        
        if (courier.ActiveOrdersCount <= 0)
            throw new ArgumentException("Количество активных заказов не может быть меньше нуля");

        await _courierRepository.AdjustActiveOrdersCount(courier.Id, adjustment: -1);
    }
}