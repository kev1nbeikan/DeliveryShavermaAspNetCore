
using UserService.Core.Abstractions;
using UserService.DataAccess.Repositories;

namespace UserService.Main.Extensions;

public static class ApiDependenciesExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, Application.UserService>();
        
        return services;
    }
}