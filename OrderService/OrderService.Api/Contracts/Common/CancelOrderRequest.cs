namespace OrderService.Api.Contracts.Common;

/// <summary>
/// Dto, представляющее запрос на отмену заказа.
/// </summary>
/// <param name="ReasonOfCanceled">Текст причины отмены заказа.</param>
public record CancelOrderRequest(string ReasonOfCanceled);