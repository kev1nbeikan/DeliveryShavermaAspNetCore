using System.Diagnostics;
using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Services;
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
    private readonly ICurierService _curierService;
    private readonly IStoreService _storeService;
    private readonly IOrderService _orderService;

    public PaymentController(ILogger<PaymentController> logger,
        IUserService userService,
        IPaymentService paymentService,
        IMenuService menuService,
        IHandlerOrderService handlerOrderService,
        ICurierService curierService,
        IStoreService storeService,
        IOrderService orderService)
    {
        _logger = logger;
        _userService = userService;
        _paymentService = paymentService;
        _menuService = menuService;
        _handlerOrderService = handlerOrderService;
        _curierService = curierService;
        _storeService = storeService;
        _orderService = orderService;
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

        var (curier, deliveryTime) = await _curierService.GetCurier(handlerServiceOrder.ClientAddress);

        if (curier == null) return BadRequest("Curier is not found");

        (var cookingTime, error) =
            await _storeService.GetCookingTime(handlerServiceOrder.StoreId, handlerServiceOrder.Basket);
        if (error.IsNotEmptyOrNull()) return BadRequest(error);

        (error, var cheque) = _paymentService.ConfirmPayment(handlerServiceOrder, paymentConfirmRequest.PaymentType,
            paymentConfirmRequest.CardNumber,
            paymentConfirmRequest.ExpiryDate, paymentConfirmRequest.CVV, paymentConfirmRequest.Comment,
            paymentConfirmRequest.Address);

        if (error.IsNotEmptyOrNull()) return BadRequest(error);

        (var myUser, error) = await _userService.Get(User.UserId());

        if (error.IsNotEmptyOrNull()) return BadRequest(error);

        (var order, error) = Order.Create(
            handlerServiceOrder.Id,
            Handler.Core.StatusCode.Cooking,
            handlerServiceOrder.Basket.ToOrderBucket(),
            handlerServiceOrder.Price,
            handlerServiceOrder.Comment,
            cheque!,
            handlerServiceOrder.ClientAddress,
            curier.PhoneNumber,
            myUser!.PhoneNumber,
            myUser.UserId,
            handlerServiceOrder.StoreId,
            handlerServiceOrder.StoreId,
            cookingTime,
            deliveryTime,
            default,
            default,
            default
        );

        if (error.IsNotEmptyOrNull()) return BadRequest(error);

        error = await _orderService.Save(order);
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