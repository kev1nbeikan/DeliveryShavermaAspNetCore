using UserService.Core;
using UserService.Core.Abstractions;
using UserService.Core.Exceptions;

namespace UserService.Application;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Получает пользователя по его ID.
    /// </summary>
    /// <param name="userId">ID пользователя.</param>
    /// <returns>Объект пользователя, если он найден.</returns>
    /// <exception cref="NotFoundException">Возникает, если пользователь не найден.</exception>
    public async Task<MyUser> Get(Guid userId)
    {
        var user = await _userRepository.Get(userId);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        return user;
    }

    /// <summary>
    /// Сохраняет нового пользователя.
    /// </summary>
    /// <param name="user">Объект пользователя.</param>
    /// <exception cref="ArgumentException">Возникает, если объект пользователя не валиден.</exception>
    public async Task Save(MyUser user)
    {
        await _userRepository.Add(user);
    }

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
    public async Task<MyUser> Upsert(Guid userId, string address, string phoneNumber, string comment)
    {
        var newUser = CreateUserModel(userId, new List<string> { address }, phoneNumber, comment);

        var existingUser = await _userRepository.Get(userId);

        if (existingUser == null)
        {
            return await _userRepository.Add(newUser);
        }

        return await UpdateExistingUser(existingUser, address, phoneNumber, comment);
    }

    /// <summary>
    /// Добавляет нового пользователя.
    /// </summary>
    /// <param name="userId">ID пользователя.</param>
    /// <param name="addresses">Список адресов пользователя.</param>
    /// <param name="phoneNumber">Номер телефона пользователя.</param>
    /// <param name="comment">Комментарий пользователя.</param>
    /// <returns>Объект пользователя.</returns>
    /// <exception cref="ArgumentException">Возникает, если данные пользователя не валидны.</exception>
    public async Task<MyUser> Add(Guid userId, List<string> addresses, string phoneNumber, string comment)
    {
        var user = CreateUserModel(userId, addresses, comment, phoneNumber);

        await _userRepository.Add(user);

        return user;
    }

    private MyUser CreateUserModel(Guid userId, List<string> address, string phoneNumber, string comment)
    {
        var createUserResult = MyUser.Create(userId, address, comment, phoneNumber);
        if (!string.IsNullOrEmpty(createUserResult.error))
        {
            throw new ArgumentException(createUserResult.error);
        }

        return createUserResult.myUser!;
    }

    private async Task<MyUser> UpdateExistingUser(MyUser existingUser, string newAddress, string newPhoneNumber,
        string newComment)
    {
        if (await _userRepository.Update(existingUser, newAddress, newComment, newPhoneNumber))
        {
            return existingUser.Update(newAddress, newPhoneNumber, newComment);
        }

        throw new FailToUpdateRepositoryException<MyUser>("fail to update user", existingUser);
    }
}