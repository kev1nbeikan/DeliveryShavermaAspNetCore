using BarsGroupProjectN1.Core.Models;
using Handler.Core.Common;
using UserService.Core;

namespace Handler.Core.Abstractions.Services;

public interface IUserService
{
    /// <summary>
    /// Обновляет информацию о пользователе или добавляет нового пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="address">Адрес пользователя.</param>
    /// <param name="comment">Комментарий к пользователю.</param>
    /// <param name="phoneNumber">Номер телефона пользователя.</param>
    /// <returns>Возвращает ошибку, если есть, иначе null.</returns>
    Task<string?> Upsert(Guid userId, string address, string comment, string phoneNumber);
    Task<(MyUser?, string? error)> Get(Guid userId);
}