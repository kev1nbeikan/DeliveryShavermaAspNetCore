using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Abstractions.UseCases;
using HandlerService.Application.Services;
using HandlerService.Application.UseCases;
using HandlerService.Controllers;
using HandlerService.DataAccess.Repositories;

namespace HandlerService.Extensions;

public static class AppServiceDependenciesExtensions
{
    public static void AddDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddMenuDependencies();
        serviceProvider.AddOrderDependencies();
        serviceProvider.AddHandlerDependencies();
        serviceProvider.AddCurierDependencies();
        serviceProvider.AddUserDependencies();
        serviceProvider.AddStoreDependencies();
        serviceProvider.AddPaymentDependencies();
        serviceProvider.AddSingleton<IGetOrderLogisticUseCase, GetOrderLogisticUseCase>();
    }

    private static void AddMenuDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IMenuService, MenuService>();
        serviceProvider.AddSingleton<IMenuRepository, MenuRepository>();
        serviceProvider.AddHttpClient<MenuRepository>();

    }

    private static void AddOrderDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IOrderService, OrderService>();
        serviceProvider.AddSingleton<IOrderRepository, OrderRepository>();
        serviceProvider.AddHttpClient<OrderRepository>();

    }

    private static void AddHandlerDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IHandlerRepository, HandlerRepository>();
        serviceProvider.AddSingleton<IHandlerOrderService, HandlerOrderService>();
        serviceProvider.AddHttpClient<HandlerRepository>();
    }
    
    private static void AddPaymentDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IPaymentService, PaymentService>();
    }

    private static void AddCurierDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<ICurierService, CurierService>();
        serviceProvider.AddSingleton<ICurierRepository, CurierRepository>();
        serviceProvider.AddHttpClient<CurierRepository>();

    }

    private static void AddUserDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IUserService, UserService>();
        serviceProvider.AddSingleton<IUserRepository, UserRepository>();
        serviceProvider.AddHttpClient<UserRepository>();

    }

    private static void AddStoreDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IStoreService, StoreService>();
        serviceProvider.AddSingleton<IStoreRepository, StoreRepository>();
        serviceProvider.AddHttpClient<StoreRepository>();

    }
}