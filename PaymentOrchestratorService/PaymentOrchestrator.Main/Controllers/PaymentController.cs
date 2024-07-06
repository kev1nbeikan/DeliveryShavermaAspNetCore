using System.Diagnostics;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Contracts.Orders;
using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Services;
using Handler.Core.Abstractions.UseCases;
using Handler.Core.Common;
using Handler.Core.Contracts;
using Handler.Core.Exceptions;
using Handler.Core.Extensions;
using Handler.Core.HanlderService;
using HandlerService.Contracts;
using HandlerService.Extensions;
using HandlerService.Infustucture.Extensions;
using Microsoft.AspNetCore.Mvc;
using HandlerService.Models;
using Microsoft.Extensions.Options;

namespace HandlerService.Controllers;

[Route("[controller]/[action]")]
public class PaymentController : Controller
{
    private readonly ILogger<PaymentController> _logger;
    private readonly IPaymentService _paymentService;
    private readonly IPaymentUseCases _paymentUseCases;
    private readonly IPaymentOrderService _paymentOrderService;
    private readonly IOptions<ServicesOptions> _options;

    public PaymentController(ILogger<PaymentController> logger,
        IPaymentUseCases paymentUseCases, IPaymentService paymentService, IOptions<ServicesOptions> options,
        IPaymentOrderService paymentOrderService)
    {
        _logger = logger;
        _paymentUseCases = paymentUseCases;
        _paymentService = paymentService;
        _options = options;
        _paymentOrderService = paymentOrderService;
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> PaymentConfirm([FromBody] PaymentConfirmRequest paymentConfirmRequest)
    {
        try
        {
            
            var userId = User.UserId();

            _logger.LogInformation($"User {userId} requested PaymentConfirm with body {paymentConfirmRequest}");

            await _paymentUseCases
                .ExecutePaymentConfirm(
                    paymentConfirmRequest.OrderId,
                    userId,
                    paymentConfirmRequest.ToPaymentInfo()
                );

            return Redirect($"{_options.Value.UsersUrl}/user/currentorder");
        }
        catch (PaymentConfirmException e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> PaymentBuild([FromBody] PaymentRequest paymentRequest)
    {
        try
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

            return RedirectToAction("Payment", new { paymentOrderId = paymentOrder!.Id });
            
        }
        catch (PaymentBuildException e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet("{paymentOrderId:Guid}")]
    public IActionResult Payment(Guid paymentOrderId)
    {
        var paymentOrder = _paymentOrderService.Get(paymentOrderId);
        if (paymentOrder == null)
            return BadRequest("Страница платежа не найдена");

        var viewModel = new PaymentViewModel()
        {
            Order = paymentOrder!,
            Products = paymentOrder.ProdutsList,
            Price = paymentOrder.Price,
            PaymentTypes = _paymentService.GetPaymentTypes()
        };
        

        return View(new PaymentViewModel()
        {
            Order = paymentOrder!,
            Products = paymentOrder.ProdutsList,
            Price = paymentOrder.Price,
            PaymentTypes = _paymentService.GetPaymentTypes()
        });
    }
}