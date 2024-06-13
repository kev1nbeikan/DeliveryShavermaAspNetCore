using System.Runtime.InteropServices.JavaScript;
using Handler.Core;
using Handler.Core.Abstractions;

namespace HandlerService.Application.Services;

public class MenuService : IMenuService
{
    public (Product[] products, string? error) GetProducts(List<long> productIds)
    {
        var products = new Product[productIds.Count];

        for (int i = 0; i < productIds.Count; i++)
        {
            products[i] = Product.Create(
                productIds[i],
                "title",
                100,
                "desc", new List<string>(),
                Guid.NewGuid()).product!;
        }


        return (products, null);
    }
}