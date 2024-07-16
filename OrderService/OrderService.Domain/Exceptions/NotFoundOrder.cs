using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;

namespace OrderService.Domain.Exceptions;

/// <summary>
/// Исключение, возникающее при отсутствии заказа с заданным ID и условиями.
/// </summary>
public class NotFoundOrder : KeyNotFoundException
{
    /// <summary>
    /// Дополнительные данные об ошибке.
    /// </summary>
    public Dictionary<string, object>? AdditionalData { get; private set; }

    public NotFoundOrder(Dictionary< string,object> add, string methodName)
        : base($"Заказ с этим id и условиями не найден. Метод = {methodName}")
    {
        AdditionalData = add;
    }
    
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="NotFoundOrder"/> с указанием дополнительной информации с ролью, идентификатором источника, идентификатором заказа и имени метода, где произошла ошибка.
    /// </summary>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="sourceId">Идентификатор источника (клиент, курьер, магазин).</param>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <param name="methodName">Имя метода, где произошла ошибка.</param>
    public NotFoundOrder(RoleCode role, Guid sourceId, Guid orderId, string methodName)
        : base($"Заказ с этим id и условиями не найден. Метод = {methodName}")
    {
        AdditionalData = new Dictionary<string, object>
        {
            { "Role", role },
            { "SourceId", sourceId },
            { "OrderId", orderId },
        };
    }
}