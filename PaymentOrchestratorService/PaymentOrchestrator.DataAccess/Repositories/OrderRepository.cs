using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Contracts.Orders;
using BarsGroupProjectN1.Core.Repositories;
using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HandlerService.DataAccess.Repositories;

public class OrderRepository : RepositoryHttpClientBase, IOrderRepository
{
    public OrderRepository(IHttpClientFactory clientFactory, IOptions<ServicesOptions> options) :
        base(options.Value.OrderUrl, clientFactory)
    {
    }

    public async Task<string?> Save(OrderCreateRequest order)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("orders", order);

        if (!response.IsSuccessStatusCode)
        {
            return "Ошибка при создании заказа: " + await response.Content.ReadAsStringAsync();
        }


        return null;
    }
}