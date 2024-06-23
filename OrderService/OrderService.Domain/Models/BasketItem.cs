namespace OrderService.Domain.Models;

public abstract class BasketItem
{
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
    public int Price { get; set; }
}