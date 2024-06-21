using Handler.Core.Contracts;
using Refit;

namespace HandlerService.DataAccess.Repositories.MessageHandler;

public interface IServiceClient
{
    [Post("/user/AddNewOrUpdate/AddNewOrUpdate")]
    Task<string> Post(UpsertFields fields);
}