using System.Text.Json;
using CourierService.Core.Abstractions;
using CourierService.Core.Models;
using Microsoft.Extensions.Configuration;

namespace CourierService.Application.Services;

public class OrdersApiClient : IOrdersApiClient
{
	private readonly HttpClient _httpClient;

	private readonly IConfiguration _configuration;

	public OrdersApiClient(HttpClient httpClient, IConfiguration configuration)
	{
		_httpClient = httpClient;
		_configuration = configuration;
	}

	public async Task<CourierGetCurrent> GetLatestOrderAsync()
	{
		var baseUrl = _configuration["OrdersApi:BaseUrl"];

		var response = await _httpClient.GetAsync($"{baseUrl}/latest_order");

		if (!response.IsSuccessStatusCode)
		{
			throw new Exception("Ошибка при получении данных из OrdersApi");
		}

		var json = await response.Content.ReadAsStringAsync();

		var result = JsonSerializer.Deserialize<CourierGetCurrent>(
			json,
			new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			}
		);

		return result;
	}

	public async Task<List<CourierGetCurrent>> GetCurrentOrdersAsync()
	{
		var baseUrl = _configuration["OrdersApi:BaseUrl"];
		var response = await _httpClient.GetAsync($"{baseUrl}/current");

		if (!response.IsSuccessStatusCode)
		{
			throw new Exception("Ошибка при получении данных из OrdersApi");
		}

		var json = await response.Content.ReadAsStringAsync();

		var result = JsonSerializer.Deserialize<List<CourierGetCurrent>>(
			json,
			new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			}
		);

		return result;
	}
}