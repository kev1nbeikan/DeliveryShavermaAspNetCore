using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Models;

namespace OrderService.UnitTests.Utils;

public static class TestExtensions
{
    public static bool IsEqualOrder(this CurrentOrder order1, CurrentOrder order2)
    {
        return order1.Id == order2.Id &&
               order1.ClientId == order2.ClientId &&
               order1.CourierId == order2.CourierId &&
               order1.StoreId == order2.StoreId &&
               IsEqualBasket(order1.Basket, order2.Basket) &&
               order1.Price == order2.Price &&
               order1.Comment == order2.Comment &&
               order1.StoreAddress == order2.StoreAddress &&
               order1.ClientAddress == order2.ClientAddress &&
               order1.CourierNumber == order2.CourierNumber &&
               order1.ClientNumber == order2.ClientNumber &&
               order1.CookingTime == order2.CookingTime &&
               order1.DeliveryTime == order2.DeliveryTime &&
               order1.OrderDate == order2.OrderDate &&
               order1.CookingDate == order2.CookingDate &&
               order1.DeliveryDate == order2.DeliveryDate &&
               order1.Cheque == order2.Cheque &&
               order1.Status == order2.Status;
    }

    public static bool IsEqualBasket( List<BasketItem> basket1, List<BasketItem> basket2)
    {
        if (basket1.Count != basket2.Count)
        {
            return false;
        }

        for (int i = 0; i < basket1.Count; i++)
        {
            if (basket1[i].ProductId != basket2[i].ProductId ||
                basket1[i].Name != basket2[i].Name ||
                basket1[i].Amount != basket2[i].Amount ||
                basket1[i].Price != basket2[i].Price)
            {
                return false;
            }
        }

        return true;
    }
}