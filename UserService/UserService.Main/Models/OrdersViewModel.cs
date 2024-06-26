using UserService.Main.Contracts;

namespace UserService.Main.Models;

public class OrdersViewModel
{
    public List<OrderGetResponse> Orders { get; set; } = [];
}