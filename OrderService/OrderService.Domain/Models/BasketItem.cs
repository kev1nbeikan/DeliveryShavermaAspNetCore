namespace OrderService.Domain.Models;

public struct BasketItem
{
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
    public int Price { get; set; }
}