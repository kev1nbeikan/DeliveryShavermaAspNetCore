using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserService.Core.Abstractions;

public interface IUserService
{
    Task<MyUser> Get(Guid userId);
    Task<MyUser> AddNewOrUpdate(Guid userId, string address, string phoneNumber,
        string comment);

    Task<MyUser> Add(Guid userId, List<string> addresses, string phoneNumber,
        string comment);
}