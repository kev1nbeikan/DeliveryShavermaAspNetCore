using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Services;
using HandlerService.Application.Services;

namespace HandlerService.Extensions;

public static class AppServiceExtensions
{
    public static void AddDependencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IMenuService, MenuService>();
        serviceProvider.AddSingleton<IUserService, UserService>();
        serviceProvider.AddSingleton<IPaymentService, PaymentService>();
        serviceProvider.AddSingleton<IHandlerOrderService, HandlerOrderService>();
    }
}