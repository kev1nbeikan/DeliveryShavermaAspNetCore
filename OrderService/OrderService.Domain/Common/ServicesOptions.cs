namespace OrderService.Domain.Common;

/// <summary>
/// Опции для сервисов (для подключения CORS.
/// </summary>
public class ServicesOptions
{
    public string CouriersUrl { get; set; } = String.Empty;
    public string StoreUrl { get; set; } = String.Empty;
    public string UsersUrl { get; set; } = String.Empty;
    public string MenuUrl { get; set; } = String.Empty;
    public string PaymentOrchestratorUrl { get; set; } = String.Empty;
}