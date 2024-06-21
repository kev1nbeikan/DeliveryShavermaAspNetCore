using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using Handler.Core.Contracts;
using HandlerService.DataAccess.Repositories.MessageHandler;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace HandlerService.DataAccess;

public class UserRepoRefit : IUserRepository
{
    private readonly IServiceClient _client;

    public UserRepoRefit(IServiceProvider serviceProvider)
    {
        _client = serviceProvider.GetRequiredService<IServiceClient>();
    }


    public Task<MyUser?> Get(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<string?> Save(MyUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<string?> Upsert(UpsertFields fields)
    {
        return await _client.Post(fields);
    }
}