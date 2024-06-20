using CourierService.Core.Models;

namespace CourierService.Core.Abstractions;

public interface IOrdersApiClient
{
	Task<CourierGetCurrent> GetLatestOrderAsync();
}