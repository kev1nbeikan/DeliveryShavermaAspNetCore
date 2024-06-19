namespace UserService.Core.abstractions;

public interface IUserService
{
    Task<MyUser> Get(Guid userId);
    Task<MyUser> AddNewOrUpdate(Guid userId, string address, string phoneNumber,
        string comment);

    Task<MyUser> Add(Guid userId, List<string> addresses, string phoneNumber,
        string comment);
}