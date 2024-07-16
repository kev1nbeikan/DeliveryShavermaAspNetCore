using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Exceptions;
using System.Data.Common;

namespace OrderService.Api.Middleware;

/// <summary>
/// Middleware для обработки исключений.
/// </summary>
/// <param name="logger"></param>
public sealed class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger = logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex) when (ex.InnerException is Npgsql.NpgsqlException)
        {
            switch (ex)
            {
                case DbUpdateException:
                    await WriteErrorResponseAsync(context, StatusCodes.Status400BadRequest,
                        "Не получается обновить базу данных", ex.GetType().ToString(), GetAdditionalData(ex));
                    return;
                default:
                    var (message, data) = OnUncaughtException(ex, context);
                    await WriteErrorResponseAsync(context, StatusCodes.Status500InternalServerError,
                        message + ", связанная с базой данных", ex.GetType().ToString(), data);
                    return;
            }
        }
        catch (Exception ex)
        {
            switch (ex)
            {
                case FailToCreateOrderModel:
                    await WriteErrorResponseAsync(context, StatusCodes.Status422UnprocessableEntity, ex.Message,
                        ex.GetType().ToString());
                    return;
                case FailToUseOrderRepository:
                    await WriteErrorResponseAsync(context, StatusCodes.Status422UnprocessableEntity, ex.Message,
                        ex.GetType().ToString());
                    return;
                case FailToChangeStatus:
                    await WriteErrorResponseAsync(context, StatusCodes.Status400BadRequest, ex.Message,
                        ex.GetType().ToString());
                    return;
                case NotFoundOrder notFoundOrderEx:
                    await WriteErrorResponseAsync(context, StatusCodes.Status400BadRequest, ex.Message,
                        ex.GetType().ToString(),
                        notFoundOrderEx.AdditionalData);
                    return;
                case DbUpdateException:
                    await WriteErrorResponseAsync(context, StatusCodes.Status400BadRequest, ex.Message,
                        ex.GetType().ToString());
                    return;
                default:
                    var (message, data) = OnUncaughtException(ex, context);
                    await WriteErrorResponseAsync(context, StatusCodes.Status500InternalServerError, message,
                        ex.GetType().ToString(), data);
                    return;
            }
        }
    }

    /// <summary>
    /// Обработчик необработанных исключений.
    /// </summary>
    /// <param name="ex">Исключение, которое было вызвано.</param>
    /// <param name="context">Текущий контекст HTTP-запроса.</param>
    /// <returns>Кортеж, содержащий сообщение об ошибке и дополнительную информацию.</returns>
    private (string Message, Dictionary<string, object>? data) OnUncaughtException(Exception ex, HttpContext context)
    {
        if (ex is NotImplementedException)
        {
            return ("Функционал ещё не реализован", null);
        }

        var errorId = Activity.Current?.Id ?? GenerateRandomId();

        var additionalData = GetAdditionalData(ex);
        additionalData.Add("ErrorId", errorId);

        return ($"Произошла неизвестная ошибка: [#{errorId}]", additionalData);
    }

    /// <summary>
    /// Записывает ответ об ошибке в поток ответа.
    /// </summary>
    /// <param name="context">Текущий контекст HTTP-запроса.</param>
    /// <param name="statusCode">Код состояния HTTP-ответа.</param>
    /// <param name="message">Сообщение об ошибке.</param>
    /// <param name="type">Тип ошибки.</param>
    /// <param name="additionalData">Дополнительная информация об ошибке (необязательно).</param>
    /// <returns>Задача, представляющая асинхронную операцию записи ответа.</returns>
    private Task WriteErrorResponseAsync(HttpContext context, int statusCode, string message, string type,
        Dictionary<string, object>? additionalData = null)
    {
        if (additionalData == null)
        {
            _logger.LogError("{Path}, {StatusCode}, {Message}, {Type}", context.Request.Path, statusCode, message,
                type);
        }
        else
        {
            _logger.LogError("{Path}, {StatusCode}, {Message}, {Type}, Дополнительные данные: {@AdditionalData}",
                context.Request.Path, statusCode, message, type, additionalData);
        }

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsJsonAsync(new { message });
    }

    /// <summary>
    /// Возвращает словарь с дополнительной информацией об исключении.
    /// </summary>
    /// <param name="ex">Исключение.</param>
    /// <returns>Словарь с дополнительной информацией.</returns>
    private static Dictionary<string, object> GetAdditionalData(Exception ex) => new()
    {
        { "Data", ex.Data },
        { "Message", ex.Message },
        { "Inner", ex.InnerException == null ? "null" : ex.InnerException },
        { "Stack", ex.StackTrace ?? "null" }
    };

    /// <summary>
    /// Генерирует случайный идентификатор длиной 10 символов.
    /// </summary>
    /// <returns>Случайный идентификатор.</returns>
    private static string GenerateRandomId()
        => Guid.NewGuid().ToString("N")[..10];
}