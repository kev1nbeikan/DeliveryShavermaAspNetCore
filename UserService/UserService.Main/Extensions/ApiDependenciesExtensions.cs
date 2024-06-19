using AuthService.Application.Services;
using AuthService.Core.Abstractions;
using UserService.Core.abstractions;
using UserService.DataAccess.Repositories;

namespace UserService.Main.Extensions;

public static class ApiDependenciesExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IUserService, Application.UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}