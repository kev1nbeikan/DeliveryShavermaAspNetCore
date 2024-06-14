using System.Diagnostics;
using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Services;
using Handler.Core.Abstractions.UseCases;
using Handler.Core.Extensions;
using HandlerService.Contracts;
using HandlerService.Extensions;
using HandlerService.Infustucture.Extensions;
using Microsoft.AspNetCore.Mvc;
using HandlerService.Models;

namespace HandlerService.Controllers;

public class PaymentController : Controller
{
    private readonly ILogger<PaymentController> _logger;
    private readonly IUserService _userService;
    private readonly IMenuService _menuService;
    private readonly IPaymentService _paymentService;
    private readonly IHandlerOrderService _handlerOrderService;
    private readonly IOrderService _orderService;
    private readonly IGetOrderTimingUseCase _getOrderTimings;

    public PaymentController(ILogger<PaymentController> logger,
        IUserService userService,
        IPaymentService paymentService,
        IMenuService menuService,
        IHandlerOrderService handlerOrderService,
        IOrderService orderService,
        IGetOrderTimingUseCase getOrderTimings)
    {
        _logger = logger;
        _userService = userService;
        _paymentService = paymentService;
        _menuService = menuService;
        _handlerOrderService = handlerOrderService;
        _orderService = orderService;
        _getOrderTimings = getOrderTimings;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult CalculatePayment([FromBody] PaymentRequest paymentRequest)
    {
        return RedirectToAction("Payment", paymentRequest);
    }

    [HttpPost]
    public async Task<IActionResult> PaymentConfirm(PaymentConfirmRequest paymentConfirmRequest)
    {
        string? error = null;

        var handlerServiceOrder = _handlerOrderService.Get(paymentConfirmRequest.OrderId);
        if (handlerServiceOrder == null) return BadRequest(error);

        (var orderTimings, error) = await _getOrderTimings.Invoke(handlerServiceOrder);
        if (error.IsNotEmptyOrNull()) return BadRequest(error);

        (error, var cheque) = _paymentService.ConfirmPayment(handlerServiceOrder, paymentConfirmRequest.ToPayment());
        if (error.IsNotEmptyOrNull()) return BadRequest(error);

        (var myUser, error) = await _userService.Get(User.UserId());
        if (error.IsNotEmptyOrNull()) return BadRequest(error);

        (var order, error) = await _orderService.CreateOrder(
            handlerServiceOrder.Id,
            handlerServiceOrder.Basket,
            handlerServiceOrder.Price,
            handlerServiceOrder.Comment,
            cheque!,
            handlerServiceOrder.ClientAddress,
            orderTimings.DeliveryTime.Agent,
            myUser!,
            handlerServiceOrder.StoreId,
            orderTimings.CookingTime.Time,
            orderTimings.DeliveryTime.Time
        );

        if (error.IsNotEmptyOrNull()) return BadRequest(error);

        return Ok(order);
    }


    public async Task<IActionResult> Payment(PaymentRequest paymentRequest)
    {
        var userId = User.UserId();

        var error = await _userService.Save(userId, paymentRequest.Address, paymentRequest.Comment);
        if (error.IsNotEmptyOrNull()) return BadRequest(error);

        (var products, error) = _menuService.GetProducts(paymentRequest.ProductIds);
        if (error.IsNotEmptyOrNull()) return BadRequest(error);

        var price = _paymentService.CalculatePayment(products!);

        (var order, error) = _handlerOrderService.Save(Guid.NewGuid(), userId, products, price, paymentRequest.Address,
            paymentRequest.Comment);
        if (error.IsNotEmptyOrNull()) return BadRequest(error);


        return View(new PaymentViewModel()
        {
            Order = order!,
            PaymentRequest = paymentRequest!,
            Products = products,
            Price = price!,
            PaymentTypes = _paymentService.GetPaymentTypes()
        });
    }
}