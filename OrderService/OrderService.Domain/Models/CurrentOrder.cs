using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Exceptions;

namespace OrderService.Domain.Models;

/// <summary>
/// Модель текущего заказа.
/// </summary>
public class CurrentOrder : BaseOrder
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="CurrentOrder"/>.
    /// </summary>
    private CurrentOrder(Guid id, Guid clientId, Guid courierId, Guid storeId,
        List<BasketItem> basket, int price, string comment, string storeAddress,
        string clientAddress, string courierNumber, string clientNumber,
        TimeSpan cookingTime, TimeSpan deliveryTime, DateTime orderDate,
        DateTime? cookingDate, DateTime? deliveryDate, string cheque, StatusCode status)
        : base(id, clientId, courierId, storeId, basket, price, comment,
            cookingTime, deliveryTime, orderDate, cookingDate, deliveryDate, cheque)
    {
        Status = status;
        StoreAddress = storeAddress;
        ClientAddress = clientAddress;
        CourierNumber = courierNumber;
        ClientNumber = clientNumber;
    }


    /// <summary> Статус заказа. </summary>
    public StatusCode Status { get; }

    /// <summary> Адрес магазина. </summary>
    public string StoreAddress { get; } = string.Empty;

    /// <summary> Адрес клиента. </summary>
    public string ClientAddress { get; } = string.Empty;

    /// <summary> Номер телефона курьера. </summary>
    public string CourierNumber { get; } = string.Empty;

    /// <summary> Номер телефона клиента. </summary>
    public string ClientNumber { get; } = string.Empty;

    /// <summary>
    /// Создает новый текущий заказ.
    /// </summary>
    /// <returns>Новый текущий заказ.</returns>
    /// <exception cref="FailToCreateOrderModel">Исключение выбрасывается, если данные заказа некорректны.</exception>
    public static CurrentOrder Create(Guid id, Guid clientId,
        Guid courierId, Guid storeId, List<BasketItem> basket, int price, string comment,
        string storeAddress, string clientAddress, string courierNumber, string clientNumber,
        TimeSpan cookingTime, TimeSpan deliveryTime, DateTime orderDate,
        DateTime? cookingDate, DateTime? deliveryDate, string cheque, StatusCode status)
    {
        Check(id, clientId, courierId, storeId, basket,
            price, comment, cookingTime,
            deliveryTime, cheque);

        if (string.IsNullOrEmpty(clientAddress) || clientAddress.Length > MaxAddressLength)
            throw new FailToCreateOrderModel(
                "Ошибка в аддрессе клиента, это поле не может быть пустым или превышает максимальное значение");

        if (string.IsNullOrEmpty(clientNumber) || !RegexForNumber.IsMatch(clientNumber))
            throw new FailToCreateOrderModel(
                "Ошибка в номере клиента, это поле не может быть пустым или имеет не допустимый формат номера");

        if (string.IsNullOrEmpty(courierNumber) || !RegexForNumber.IsMatch(courierNumber))
            throw new FailToCreateOrderModel(
                "Ошибка в номере курьера, это поле не может быть пустым или имеет не допустимый формат номера");

        if (string.IsNullOrEmpty(storeAddress) || storeAddress.Length > MaxAddressLength)
            throw new FailToCreateOrderModel(
                "Ошибка в аддрессе предприятия, это поле не может быть пустым или превышает максимальное значение");
        
        if (!Enum.IsDefined(typeof(StatusCode), status))
            throw new FailToCreateOrderModel(
                "Ошибка в статусе заказа, такого статуса не существует");
        
        var order = new CurrentOrder(id, clientId, courierId, storeId, basket,
            price, comment, storeAddress, clientAddress, courierNumber, clientNumber, cookingTime,
            deliveryTime, orderDate, cookingDate, deliveryDate, cheque, status);

        return order;
    }
}