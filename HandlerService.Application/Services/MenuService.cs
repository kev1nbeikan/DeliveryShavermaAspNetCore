using System.Runtime.InteropServices.JavaScript;
using Handler.Core;
using Handler.Core.Abstractions;

namespace HandlerService.Application.Services;

public class MenuService : IMenuService
{
    public (Product[] products, string? error) GetProducts(List<Guid> productIds)
    {
        var products = new Product[productIds.Count];

        for (int i = 0; i < productIds.Count; i++)
        {
            products[i] = Product.Create(
                productIds[i],
                "title",
                "bebrae",
                "desc",
                100,
                "123123123"
            ).product;
        }


        return (products, null);
    }
}