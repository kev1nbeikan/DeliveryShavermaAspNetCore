namespace BarsGroupProjectN1.Core.Models.Order;

public struct BasketItem
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public int Price { get; set; }
}