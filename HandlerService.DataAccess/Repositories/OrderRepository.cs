using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Handler.Core.Abstractions.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly string _orderUrl;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public OrderRepository(string orderUrl, IConfiguration configuration)
    {
        _orderUrl = orderUrl;

        _configuration = configuration;

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(configuration["orderUrl"] ?? throw new Exception("orderUrl not found"));
    }

    public async Task<string?> Save(Order order)
    {
        var jsonOrder = JsonSerializer.Serialize(order);
        var content = new StringContent(jsonOrder, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("orders", content);

        if (response.IsSuccessStatusCode)
        {
            var orderId = await response.Content.ReadAsStringAsync();
            return orderId;
        }

        return $"Failed to save order. Status code: {response.StatusCode}";
    }
}