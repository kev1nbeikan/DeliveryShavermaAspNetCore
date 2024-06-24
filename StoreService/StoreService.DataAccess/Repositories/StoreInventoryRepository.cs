using Microsoft.EntityFrameworkCore;
using StoreService.Core;
using StoreService.Core.Abstractions;
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
        var productsInventory = await _storeDbContext.StoreProductsInventory.Where(p =>
            p.StoreId == storeId &&
            productsIds.Contains(p.ProductId)).Select(p => p.ToCore()
        ).ToListAsync();
        return productsInventory;
    }

    public async Task Add(ProductInventory productInventory)
    {
        await _storeDbContext.AddAsync(productInventory.ToEntity());
        await _storeDbContext.SaveChangesAsync();
    }

    public async Task<ProductInventory?> GetById(Guid storeId, Guid productId)
    {
        var productInventoryEntity = await _storeDbContext.StoreProductsInventory
            .Where(p =>
                p.ProductId == productId &&
                p.StoreId == storeId)
            .FirstOrDefaultAsync();
        return productInventoryEntity?.ToCore();
    }

    public async Task<bool> Update(ProductInventory productInventory)
    {
        await _storeDbContext.StoreProductsInventory
            .Where(p =>
                p.ProductId == productInventory.ProductId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Quantity, p => productInventory.Quantity)
            );
        return await _storeDbContext.SaveChangesAsync() > 0;
    }
}