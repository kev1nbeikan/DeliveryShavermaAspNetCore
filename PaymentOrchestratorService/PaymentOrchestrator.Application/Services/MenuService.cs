using System.Dynamic;
using System.Runtime.InteropServices.JavaScript;
using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using HandlerService.Infustucture.Extensions;

namespace HandlerService.Application.Services;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<(Product[] products, string? error)> GetProducts(List<Guid> productIds)
    {
        var (products, error) = await _menuRepository.Get(productIds);
        if (error.HasValue()) return ([], error);

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