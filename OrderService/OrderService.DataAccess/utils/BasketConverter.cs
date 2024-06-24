using Newtonsoft.Json;
using OrderService.Domain.Models;

namespace OrderService.DataAccess.utils;

public static class BasketConverter
{
    public static List<BasketItem> ToBasketItem(string basketString)
    {
        return JsonConvert.DeserializeObject<List<BasketItem>>(basketString) ?? [];
    }
    
    public static string ToBasketString(List<BasketItem> basket)
    {
        return JsonConvert.SerializeObject(basket);
    }
}