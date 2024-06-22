using static System.Web.HttpUtility;
using HandlerService.Infustucture.Extensions;

namespace HandlerService.Infustucture;

public static class QueryUtils
{
    public static string GetQueryString(List<Guid> productIds)
    {
        var query = ParseQueryString(string.Empty);

        foreach (var productId in productIds)
        {
            query.Add("Guids", productId.ToString());
        }

        return query.ToString().MakeEmptyIfNull();
    }
}