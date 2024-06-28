using StoreService.Core;
using StoreService.Core.Abstractions;

namespace StoreService.Application;

public class ProductInventoryMapper : IProductInventoryMapper
{
    private readonly IStoreInventoryRepository _storeInventoryRepository;
    private readonly IStoreServiceMenuRepository _menuRepository;

    public ProductInventoryMapper(IStoreInventoryRepository storeInventoryRepository,
        IStoreServiceMenuRepository menuRepository)
    {
        _storeInventoryRepository = storeInventoryRepository;
        _menuRepository = menuRepository;
    }

    public async Task<List<MappedProduct>> GetMappedProducts(Guid storeId)
    {
        var productInventories = await _storeInventoryRepository.GetAll(storeId);
        var allProducts = await _menuRepository.GetAll();

        var productInventoryDict = productInventories.ToDictionary(pi => pi.ProductId, pi => pi.Quantity);

        var mappedProducts = allProducts
            .Select(p => new MappedProduct
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Composition = p.Composition,
                Price = p.Price,
                ImagePath = p.ImagePath,
                Quantity = productInventoryDict.ContainsKey(p.Id) ? productInventoryDict[p.Id] : 0
            })
            .ToList();

        return mappedProducts;
    }}