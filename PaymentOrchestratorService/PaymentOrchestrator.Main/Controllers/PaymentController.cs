using System.Diagnostics;
using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Services;
using Handler.Core.Abstractions.UseCases;
using Handler.Core.Common;
using Handler.Core.Contracts;
using Handler.Core.Extensions;
using Handler.Core.HanlderService;
using HandlerService.Contracts;
using HandlerService.Extensions;
using HandlerService.Infustucture.Extensions;
using Microsoft.AspNetCore.Mvc;
using HandlerService.Models;

namespace HandlerService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PaymentController : Controller
{
    private readonly ILogger<PaymentController> _logger;
    private readonly IUserService _userService;
    private readonly IMenuService _menuService;
    private readonly IPaymentService _paymentService;
    private readonly IHandlerOrderService _temporaryOrderService;
    private readonly IOrderService _orderService;
    private readonly IGetOrderLogisticUseCase _getOrderLogistic;
    private readonly IPaymentUseCases _paymentUseCases;

    public PaymentController(ILogger<PaymentController> logger,
        IUserService userService,
        IPaymentService paymentService,
        IMenuService menuService,
        IHandlerOrderService temporaryOrderService,
        IOrderService orderService,
        IGetOrderLogisticUseCase getOrderLogistic,
        IPaymentUseCases paymentUseCases)
    {
        _logger = logger;
        _userService = userService;
        _paymentService = paymentService;
        _menuService = menuService;
        _temporaryOrderService = temporaryOrderService;
        _orderService = orderService;
        _getOrderLogistic = getOrderLogistic;
        _paymentUseCases = paymentUseCases;
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
    public async Task<IActionResult> PaymentConfirm([FromForm] PaymentConfirmRequest paymentConfirmRequest)
    {
        string? error;

        var temporyOrder = _temporaryOrderService.Get(paymentConfirmRequest.OrderId);
        _logger.LogInformation(temporyOrder?.ToString());

        if (temporyOrder == null) return BadRequest("Order not found: try make order again");

        (var orderLogistic, error) = await _getOrderLogistic.Execute(temporyOrder);
        if (error.HasValue()) return BadRequest(error);
        return Ok(orderLogistic);

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
            orderLogistic!.Delivering.Executor,
            myUser!,
            orderLogistic.Cooking.Executor.Id,
            orderLogistic.Cooking.Time,
            orderLogistic.Delivering.Time
        );

        if (error.HasValue()) return BadRequest(error);

        return Ok(order);
    }


    public async Task<IActionResult> Payment([FromBody] PaymentRequest paymentRequest)
    {
        var userId = User.UserId();
        _logger.LogInformation($"User {userId} requested Payment with body {paymentRequest}");

        var (products, price, paymentOrder) = await _paymentUseCases.ExecutePaymentBuild(
            paymentRequest.ProductIdsAndQuantity,
            paymentRequest.Comment,
            paymentRequest.Address,
            paymentRequest.PhoneNumber,
            userId
        );

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