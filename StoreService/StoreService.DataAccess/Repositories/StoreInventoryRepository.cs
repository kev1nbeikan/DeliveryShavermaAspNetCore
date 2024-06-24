using Microsoft.EntityFrameworkCore;
using StoreService.Core;
using StoreService.Core.Abstractions;
using StoreService.Core.Exceptions;
using StoreService.DataAccess.Extensions;

namespace StoreService.DataAccess.Repositories;

public class StoreInventoryRepository : IStoreInventoryRepository
{
    private readonly StoreDbContext _storeDbContext;

    public StoreInventoryRepository(StoreDbContext storeDbContext)
    {
        _storeDbContext = storeDbContext;
    }

    public async Task<List<ProductInventory>> GetByIds(Guid storeId, List<Guid> productsIds)
    {
        var productsInventory = await _storeDbContext.StoreProductsInventory.AsNoTracking().Where(p =>
            p.StoreId == storeId &&
            productsIds.Contains(p.ProductId)).Select(p => p.ToCore()
        ).ToListAsync();
        return productsInventory;
    }

    public async Task Add(ProductInventory productInventory)
    {
        await EnsureValidToAdd(productInventory);

        await _storeDbContext.StoreProductsInventory.AddAsync(productInventory.ToEntity());
        await _storeDbContext.SaveChangesAsync();
    }


    public async Task<ProductInventory?> GetById(Guid storeId, Guid productId)
    {
        var productInventoryEntity = await _storeDbContext.StoreProductsInventory.AsNoTracking()
            .Where(p =>
                p.ProductId == productId &&
                p.StoreId == storeId)
            .FirstOrDefaultAsync();
        return productInventoryEntity?.ToCore();
    }

    public async Task<bool> UpdateQuantity(ProductInventory productInventory)
    {
        var productInventoryEntity = await _storeDbContext.StoreProductsInventory
            .FirstOrDefaultAsync(
                x => x.ProductId == productInventory.ProductId && x.StoreId == productInventory.StoreId);

        if (productInventoryEntity is null) return false;

        productInventoryEntity.Quantity = productInventory.Quantity;

        return await _storeDbContext.SaveChangesAsync() > 0;
    }

    private async Task EnsureValidToAdd(ProductInventory productInventory)
    {
        if (await GetById(productInventory.StoreId, productInventory.ProductId) is not null)
        {
            throw new DuplicateEntryException<ProductInventory>(
                "You can't add the same product twice in the same store",
                nameof(StoreInventoryRepository),
                productInventory);
        }
    }
}