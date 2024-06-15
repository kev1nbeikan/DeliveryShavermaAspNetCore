namespace OrderService.Domain.Models;

public enum StatusCode
{
    Cancelled,
    Active,
    Cooking,
    WaitingCourier,
    Delivering,
    WaitingClient,
    Accepted
}
