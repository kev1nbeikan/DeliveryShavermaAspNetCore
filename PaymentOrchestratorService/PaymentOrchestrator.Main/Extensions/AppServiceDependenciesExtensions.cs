using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Abstractions.UseCases;
using HandlerService.Application.Services;
using HandlerService.Application.UseCases;
using HandlerService.Controllers;
using HandlerService.DataAccess.Repositories;
using HandlerService.Infustucture;

namespace HandlerService.Extensions;

public static class AppServiceDependenciesExtensions
{
    public static void AddDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddMenuDependencies();
        serviceProvider.AddOrderDependencies();
        serviceProvider.AddHandlerDependencies();
        serviceProvider.AddCourierDependencies();
        serviceProvider.AddUserDependencies();
        serviceProvider.AddStoreDependencies();
        serviceProvider.AddPaymentDependencies();
        serviceProvider.AddSingleton<IGetOrderLogisticUseCase, GetOrderLogisticUseCase>();
    }

    private static void AddMenuDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IMenuService, MenuServiceApi>();
        serviceProvider.AddSingleton<IMenuRepository, MenuRepositoryHttp>();
        serviceProvider.AddHttpClient<MenuRepositoryHttp>();
    }

    private static void AddOrderDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IOrderService, OrderServiceApi>();
        serviceProvider.AddSingleton<IOrderRepository, OrderRepositoryHttp>();
        serviceProvider.AddHttpClient<OrderRepositoryHttp>();
    }

    private static void AddHandlerDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IHandlerRepository, HandlerRepositoryHttp>();
        serviceProvider.AddSingleton<IPaymentOrderService, PaymentOrchestratorOrderService>();
        serviceProvider.AddHttpClient<HandlerRepositoryHttp>();
    }

    private static void AddPaymentDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IPaymentService, PaymentService>();
        serviceProvider.AddSingleton<IPaymentUseCases, PaymentUseCases>();
    }

    private static void AddCourierDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<ICourierService, CourierService>();
        serviceProvider.AddSingleton<ICourierRepository, CourierRepositoryHttp>();
        serviceProvider.AddHttpClient<CourierRepositoryHttp>();
    }

    private static void AddUserDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IUserService, Application.Services.UserServiceApi>();
        serviceProvider.AddSingleton<IUserRepository, UserRepositoryHttp>();
        serviceProvider.AddHttpClient<UserRepositoryHttp>();
    }

    private static void AddStoreDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IStoreService, StoreServiceApi>();
        serviceProvider.AddSingleton<IStoreRepository, StoreRepositoryHttp>();
    }
}