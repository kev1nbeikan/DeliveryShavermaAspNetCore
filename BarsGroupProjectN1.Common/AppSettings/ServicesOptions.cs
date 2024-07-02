namespace BarsGroupProjectN1.Core.AppSettings;

public class ServicesOptions
{
    public string OrderUrl { get; set; }
    public string CouriersUrl { get; set; }
    public string StoreUrl { get; set; }
    public string UsersUrl { get; set; }
    public string MenuUrl { get; set; }

    public string AuthUrl { get; set; } = "";
    public string PaymentOrchestratorUrl { get; set; }

    public string ChatUrl { get; set; } = "";
}