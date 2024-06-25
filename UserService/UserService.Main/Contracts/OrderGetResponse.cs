using UserService.Core;

namespace UserService.Main.Contracts;

public class OrderGetResponse
{
    public Guid Id { get; set; }
    public int Status { get; set; }
    public List<BasketItem> Basket { get; set; } = [];
    int Price { get; set; }
    string Comment { get; set; } = "";
    string ClientAddress { get; set; } = "";
    string CourierNumber { get; set; } = "";
    string ClientNumber { get; set; } = "";
    string Cheque { get; set; } = "";
}