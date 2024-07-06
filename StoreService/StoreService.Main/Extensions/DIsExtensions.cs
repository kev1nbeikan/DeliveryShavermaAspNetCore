using StoreService.Application;
using StoreService.Core.Abstractions;
using StoreService.DataAccess.Repositories;

namespace StoreService.Main.Extensions;

public static class DIsExtensions
{
    public static void AddDi(this IServiceCollection services)
    {
        services.AddScoped<IGetCookingTimeUseCase, GetCookingTimeUseCase>();
        
        services.AddScoped<IStoreInventoryRepository, StoreInventoryRepository>();
        services.AddScoped<IStoreProductsService, StoreProductService>();
        
        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<IStoreService, Application.StoreService>();

        services.AddScoped<IMenuRepositoryApi, MenuRepositoryApi>();


        services.AddScoped<IProductInventoryMapper, ProductInventoryMapper>();
    }
}