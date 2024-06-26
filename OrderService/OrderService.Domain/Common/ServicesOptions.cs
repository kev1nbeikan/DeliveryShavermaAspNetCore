namespace OrderService.Domain.Common;

public class ServicesOptions
{
    public string OrderUrl { get; set; } = String.Empty;
    public string CouriersUrl { get; set; } = String.Empty;
    public string StoreUrl { get; set; } = String.Empty;
    public string UsersUrl { get; set; } = String.Empty;
    public string MenuUrl { get; set; } = String.Empty;
    public string PaymentOrchestratorUrl { get; set; } = String.Empty;
}