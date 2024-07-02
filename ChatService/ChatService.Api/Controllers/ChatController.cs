using BarsGroupProjectN1.Core.Extensions;
using ChatService.Api.Contracts;
using ChatService.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.Api.Controllers;

public class ChatController : Controller
{
    private ILogger<ChatController> _logger;

    public ChatController(ILogger<ChatController> logger)
    {
        _logger = logger;
    }

    public IActionResult Room([FromQuery] RoomRequest request)
    {
        _logger.LogInformation("User {UserId} send message to {RecipientId} with {name}", User.UserId(),
            request.RecipientId, request.ChatName);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var viewModel = new RoomViewModel
        {
            RecipientId = request.RecipientId,
            ChatName = request.ChatName
        };


        return View(viewModel);
    }
}