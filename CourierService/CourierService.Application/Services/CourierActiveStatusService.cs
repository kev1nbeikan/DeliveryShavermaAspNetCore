using System.Text;
using CourierService.Core.Models.Code;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CourierService.Application.Services;

public class CourierActiveStatusService : BackgroundService
{
	private readonly ILogger<CourierActiveStatusService> _logger;
	private readonly IHttpClientFactory _httpClientFactory;

	public CourierActiveStatusService(
		ILogger<CourierActiveStatusService> logger,
		IHttpClientFactory httpClientFactory)
	{
		_logger = logger;
		_httpClientFactory = httpClientFactory;
	}

	protected async override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			_logger.LogInformation("CourierActiveStatusService running at: {time}", DateTimeOffset.Now);

			try
			{
				var isCourierActive = CheckCourierStatus();

				if (isCourierActive)
				{
					await SendCourierStatusToOrderService();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while checking courier status.");
			}

			await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
		}
	}

	private bool CheckCourierStatus()
	{
		// Логика для проверки статуса курьера
		return true;
	}

	private async Task SendCourierStatusToOrderService()
	{
		var httpClient = _httpClientFactory.CreateClient();

		var request = new HttpRequestMessage(HttpMethod.Post, "https://ORDER-SERVICE-URL/api/courierstatus")
		{
			Content = new StringContent(
				JsonConvert.SerializeObject(
					new
					{
						Status = CourierStatusCode.Active
					}
				),
				Encoding.UTF8,
				"application/json"
			)
		};

		var response = await httpClient.SendAsync(request);
		response.EnsureSuccessStatusCode();
	}
}