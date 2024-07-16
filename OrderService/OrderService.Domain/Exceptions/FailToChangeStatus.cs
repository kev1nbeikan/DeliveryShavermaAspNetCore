using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;

namespace OrderService.Domain.Exceptions;

/// <summary>
/// Исключение, возникающее при ошибке изменения статуса заказа.
/// </summary>
public class FailToChangeStatus : Exception
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="FailToChangeStatus"/> с указанием текущего и нового статуса.
    /// </summary>
    /// <param name="currentStatus">Текущий статус заказа.</param>
    /// <param name="newStatus">Новый статус заказа.</param>
    public FailToChangeStatus(StatusCode currentStatus, StatusCode newStatus)
        : base($"Новый статус не может иметь такое значение. Текущий статус: {currentStatus}, новый статус: {newStatus}") 
    { }
    
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="FailToChangeStatus"/> с указанием сообщения об ошибке и текущего статуса.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    /// <param name="currentStatus">Текущий статус заказа.</param>
    public FailToChangeStatus(string message, StatusCode currentStatus) 
        : base(message + $"Текущий статус = {currentStatus}")
    { }
}