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
    private readonly IHandlerOrderService _temporaryOrderService;
    private readonly IOrderService _orderService;
    private readonly IGetOrderLogisticUseCase _getOrderLogistic;

    public PaymentController(ILogger<PaymentController> logger,
        IUserService userService,
        IPaymentService paymentService,
        IMenuService menuService,
        IHandlerOrderService temporaryOrderService,
        IOrderService orderService,
        IGetOrderLogisticUseCase getOrderLogistic)
    {
        _logger = logger;
        _userService = userService;
        _paymentService = paymentService;
        _menuService = menuService;
        _temporaryOrderService = temporaryOrderService;
        _orderService = orderService;
        _getOrderLogistic = getOrderLogistic;
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

        var temporyOrder = _temporaryOrderService.Get(paymentConfirmRequest.OrderId);
        if (temporyOrder == null) return BadRequest("Order not found: try make order again");

        (var orderLogistic, error) = await _getOrderLogistic.Invoke(temporyOrder);
        if (error.HasValue()) return BadRequest(error);

        (error, var cheque) = _paymentService.ConfirmPayment(temporyOrder, paymentConfirmRequest.ToPaymentInfo());
        if (error.HasValue()) return BadRequest(error);

        (var myUser, error) = await _userService.Get(User.UserId());
        if (error.HasValue()) return BadRequest(error);

        (var order, error) = await _orderService.Save(
            temporyOrder.Id,
            temporyOrder.Basket,
            temporyOrder.Price,
            temporyOrder.Comment,
            cheque!,
            temporyOrder.ClientAddress,
            orderLogistic!.Delivery.Perfomer,
            myUser!,
            orderLogistic.Cooking.Perfomer,
            orderLogistic.Cooking.Time,
            orderLogistic.Delivery.Time
        );

        if (error.HasValue()) return BadRequest(error);

        return Ok(order);
    }


    public async Task<IActionResult> Payment(PaymentRequest paymentRequest)
    {
        var userId = User.UserId();
        _logger.LogInformation($"User {userId} requested Payment with body {paymentRequest}");
        var error = await _userService.AddNewOrUpdate(userId, paymentRequest.Address, paymentRequest.Comment,
            paymentRequest.PhoneNumber);
        if (error.HasValue()) return BadRequest(error);
        return Ok("Ok");
        //
        // (var products, error) =
        //     await _menuService.GetProducts(paymentRequest.ProductIdsAndQuantity.Select(x => x.Id).ToList());
        // if (error.HasValue()) return BadRequest(error);
        //
        // var price = _paymentService.CalculatePayment(products, paymentRequest.ProductIdsAndQuantity);
        //
        // (var paymentOrder, error) = _temporaryOrderService.Save(Guid.NewGuid(), userId, products, price,
        //     paymentRequest.Address,
        //     paymentRequest.Comment);
        // if (error.HasValue()) return BadRequest(error);
        //
        //
        // return View(new PaymentViewModel()
        // {
        //     Order = paymentOrder!,
        //     PaymentRequest = paymentRequest!,
        //     Products = products,
        //     Price = price!,
        //     PaymentTypes = _paymentService.GetPaymentTypes()
        // });
    }
}