using Handler.Core.Abstractions.Services;
using Handler.Core.Abstractions.UseCases;
using Handler.Core.Common;
using Handler.Core.Contracts;
using Handler.Core.HanlderService;
using Handler.Core.Payment;
using HandlerService.Infustucture.Extensions;

namespace HandlerService.Application.UseCases;

public class PaymentUseCases : IPaymentUseCases
{
    private readonly IUserService _userService;
    private readonly IMenuService _menuService;
    private readonly IPaymentService _paymentService;
    private readonly IHandlerOrderService _temporaryOrderService;
    private readonly IOrderService _orderService;
    private readonly IGetOrderLogisticUseCase _getOrderLogistic;

    public PaymentUseCases(IUserService userService, IMenuService menuService, IPaymentService paymentService,
        IHandlerOrderService temporaryOrderService, IOrderService orderService,
        IGetOrderLogisticUseCase getOrderLogistic)
    {
        _userService = userService;
        _menuService = menuService;
        _paymentService = paymentService;
        _temporaryOrderService = temporaryOrderService;
        _orderService = orderService;
        _getOrderLogistic = getOrderLogistic;
    }


    public async Task<(Product[] products, int price, TemporyOrder? paymentOrder)> ExecutePaymentBuild(
        List<BucketItem> productIdsAndQuantity,
        string comment,
        string address,
        string phoneNumber,
        Guid userId)
    {
        string? error;

        error = await _userService.Upsert(userId, address, comment,
            phoneNumber);
        if (error.HasValue()) throw new PaymentBuildException(error!);


        var productIds = productIdsAndQuantity.Select(x => x.Id).ToList();

        (var products, error) =
            await _menuService.GetProducts(productIds);
        if (error.HasValue()) throw new PaymentBuildException(error!);

        var price = _paymentService.CalculatePayment(products, productIdsAndQuantity);

        (var paymentOrder, error) = _temporaryOrderService.Save(
            Guid.NewGuid(),
            userId,
            products,
            price,
            address,
            comment,
            productIdsAndQuantity);
        if (error.HasValue()) throw new PaymentBuildException(error!);

        return (products, price, paymentOrder);
    }
}