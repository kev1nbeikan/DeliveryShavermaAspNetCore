using AuthService.Core.Abstractions;
using AuthService.DataAccess.Repositories;

namespace AuthService.Main.Extensions;

public static class DependenciesServicesExtensions
{
    public static IServiceCollection AddDependenciesServices(this IServiceCollection services)
    {
        services.AddScoped<IUserAuthRepo, UserAuthRepo>();
        services.AddScoped<IStoreAuthRepo, StoreAuthRepo>();
        services.AddScoped<ICurierAuthRepo, CurierAuthRepo>();

        return services;
    }
}