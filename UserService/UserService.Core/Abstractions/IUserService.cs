using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarsGroupProjectN1.Core.Models;
using UserService.Core.Exceptions;

namespace UserService.Core.Abstractions;

public interface IUserService
{
    Task<MyUser> Get(Guid userId);

    /// <summary>
    /// Обновляет или создает пользователя.
    /// </summary>
    /// <param name="userId">ID пользователя.</param>
    /// <param name="address">Адрес пользователя.</param>
    /// <param name="phoneNumber">Номер телефона пользователя.</param>
    /// <param name="comment">Комментарий пользователя.</param>
    /// <returns>Объект пользователя.</returns>
    /// <exception cref="ArgumentException">Возникает, если данные пользователя не валидны.</exception>
    /// <exception cref="FailToUpdateRepositoryException{T}">Возникает, если не удалось обновить пользователя в репозитории.</exception>
    Task<MyUser> Upsert(Guid userId, string address, string phoneNumber,
        string comment);

    Task<MyUser> Add(Guid userId, List<string> addresses, string phoneNumber,
        string comment);
}