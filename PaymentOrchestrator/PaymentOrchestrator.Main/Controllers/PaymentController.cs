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
    private readonly IHandlerOrderService _paymentOrderService;
    private readonly IOrderService _orderService;
    private readonly IGetOrderTimingUseCase _getOrderTimings;

    public PaymentController(ILogger<PaymentController> logger,
        IUserService userService,
        IPaymentService paymentService,
        IMenuService menuService,
        IHandlerOrderService paymentOrderService,
        IOrderService orderService,
        IGetOrderTimingUseCase getOrderTimings)
    {
        _logger = logger;
        _userService = userService;
        _paymentService = paymentService;
        _menuService = menuService;
        _paymentOrderService = paymentOrderService;
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
        string? error;

        var paymentOrder = _paymentOrderService.Get(paymentConfirmRequest.OrderId);
        if (paymentOrder == null) return BadRequest("Order not found: retry make order again");

        (var orderTimings, error) = await _getOrderTimings.Invoke(paymentOrder);
        if (error.IsNotEmptyOrNull()) return BadRequest(error);

        (error, var cheque) = _paymentService.ConfirmPayment(paymentOrder, paymentConfirmRequest.ToPayment());
        if (error.IsNotEmptyOrNull()) return BadRequest(error);

        (var myUser, error) = await _userService.Get(User.UserId());
        if (error.IsNotEmptyOrNull()) return BadRequest(error);

        (var order, error) = await _orderService.CreateAndSave(
            paymentOrder.Id,
            paymentOrder.Basket,
            paymentOrder.Price,
            paymentOrder.Comment,
            cheque!,
            paymentOrder.ClientAddress,
            orderTimings.DeliveryTime.Agent,
            myUser!,
            orderTimings.CookingTime.Agent,
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

        (var paymentOrder, error) = _paymentOrderService.Save(Guid.NewGuid(), userId, products, price,
            paymentRequest.Address,
            paymentRequest.Comment);
        if (error.IsNotEmptyOrNull()) return BadRequest(error);


        return View(new PaymentViewModel()
        {
            Order = paymentOrder!,
            PaymentRequest = paymentRequest!,
            Products = products,
            Price = price!,
            PaymentTypes = _paymentService.GetPaymentTypes()
        });
    }
}