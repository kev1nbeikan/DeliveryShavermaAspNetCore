using System.Diagnostics;
using Handler.Core.Abstractions;
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

    public PaymentController(ILogger<PaymentController> logger, IUserService userService,
        IPaymentService paymentService, IMenuService menuService, IHandlerOrderService handlerOrderService)
    {
        _logger = logger;
        _userService = userService;
        _paymentService = paymentService;
        _menuService = menuService;
        _handlerOrderService = handlerOrderService;
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
    public IActionResult PaymentConfirm(PaymentConfirmRequest paymentConfirmRequest)
    {
        string? error = null;
        var order = _handlerOrderService.Get(paymentConfirmRequest.OrderId);
        if (order == null) return BadRequest(error);


        (error, var check) = _paymentService.ConfirmPayment(order, paymentConfirmRequest.PaymentType,
            paymentConfirmRequest.CardNumber,
            paymentConfirmRequest.ExpiryDate, paymentConfirmRequest.CVV, paymentConfirmRequest.Comment,
            paymentConfirmRequest.Address);
        if (error.IsNotEmptyOrNull()) return BadRequest(error);


        return Ok(paymentConfirmRequest);
    }


    public IActionResult Payment(PaymentRequest paymentRequest)
    {
        var userId = User.UserId();

        var (myUser, error) = _userService.Save(userId, paymentRequest.Address, paymentRequest.Comment);
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
            Products = order!.Products,
            Price = price!,
            PaymentTypes = _paymentService.GetPaymentTypes()
        });
    }
}