using System.Dynamic;
using System.Runtime.InteropServices.JavaScript;
using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Common;
using HandlerService.Infustucture.Extensions;

namespace HandlerService.Application.Services;

public class MenuServiceApi : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuServiceApi(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<(Product[] products, string? error)> GetProducts(List<Guid> productIds)
    {
        var (products, error) = await _menuRepository.Get(productIds);
        if (error.HasValue()) return ([], error);
        return (products, null);
    }
}