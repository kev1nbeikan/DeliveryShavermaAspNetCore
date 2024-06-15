using AuthService.Application.Services;
using AuthService.Application.Utils;
using AuthService.Core.Abstractions;
using AuthService.DataAccess.Repositories;
using User.Infastructure.Abstractions;

namespace AuthService.Main.Extensions;

public static class DependenciesServicesExtensions
{
    public static IServiceCollection AddDependenciesServices(this IServiceCollection services)
    {
        services.AddScoped<IUserAuthRepo, UserAuthRepo>();
        services.AddScoped<IStoreAuthRepo, StoreAuthRepo>();
        services.AddScoped<ICourierAuthRepo, CourierAuthRepo>();

        services.AddScoped<IUserAuthService, UserAuthService>();
        services.AddScoped<ICourierAuthService, CourierAuthService>();
        services.AddScoped<IStoreAuthService, StoreAuthService>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}