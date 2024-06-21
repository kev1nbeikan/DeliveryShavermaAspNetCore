using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using Handler.Core.Contracts;
using RestSharp;

namespace HandlerService.DataAccess.Repositories;

public class UserRepoRestSharp : IUserRepository
{
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
        var client = new RestClient();
        var request = new RestRequest("https://localhost:7227/user/AddNewOrUpdate/AddNewOrUpdate")
        {
            Method = Method.Post,
            RequestFormat = DataFormat.Json
        }.AddJsonBody(fields);

        var response = await client.PostAsync(request);
        return !response.IsSuccessStatusCode ? response.Content : null;
    }
}