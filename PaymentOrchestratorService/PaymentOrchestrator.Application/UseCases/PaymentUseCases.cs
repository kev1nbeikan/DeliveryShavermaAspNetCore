using BarsGroupProjectN1.Core.Contracts.Orders;
using Handler.Core.Abstractions.Services;
using Handler.Core.Abstractions.UseCases;
using Handler.Core.Common;
using Handler.Core.Contracts;
using Handler.Core.Exceptions;
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


    public async Task<(Product[] products, int price, PaymentOrder? paymentOrder)> ExecutePaymentBuild(
        List<ProductWithAmount> productIdsAndQuantity,
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

        var bucket = products.Join(
            productIdsAndQuantity,
            product => product.Id,
            bucketItem => bucketItem.Id,
            (product, bucketItem) => (product: product, amount: bucketItem.Quantity,
                price: product.Price)).ToList();

        if (error.HasValue()) throw new PaymentBuildException(error!);

        var price = _paymentService.GetTotalPrice(products, productIdsAndQuantity);

        (var paymentOrder, error) = _temporaryOrderService.Save(
            Guid.NewGuid(),
            userId,
            products,
            price,
            address,
            comment,
            bucket,
            phoneNumber);
        if (error.HasValue()) throw new PaymentBuildException(error!);

        return (products, price, paymentOrder);
    }

    public async Task<OrderCreateRequest> ExecutePaymentConfirm(Guid orderId, Guid userId, Payment payment)
    {
        string? error;

        var temporyOrder = _temporaryOrderService.Get(orderId);

        if (temporyOrder == null)
            throw new PaymentConfirmException("Платеж не найден: перейдите на страницу магазина и попробуйте ещё раз");


        (var orderLogistic, error) = await _getOrderLogistic.Execute(temporyOrder);
        if (error.HasValue())
            throw new PaymentConfirmException(error);


        (error, var cheque) = _paymentService.ConfirmPayment(temporyOrder, payment);
        if (error.HasValue())
            throw new PaymentConfirmException(error);

        (var myUser, error) = await _userService.Get(userId);
        if (error.HasValue())
            throw new PaymentConfirmException(error);


        (var order, error) = await _orderService.Save(
            temporyOrder!,
            orderLogistic!,
            cheque!,
            myUser!
        );

        if (error.HasValue()) throw new PaymentConfirmException(error);

        return order;
    }
}