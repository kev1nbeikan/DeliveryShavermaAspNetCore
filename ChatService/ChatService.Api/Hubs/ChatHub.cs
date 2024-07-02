using System.Text.RegularExpressions;
using BarsGroupProjectN1.Core.Extensions;
using ChatService.Api.Contracts;
using ChatService.Core;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Api.Hubs;

public class ChatHub : Hub<IChatHub>
{
    private readonly IChatService _chatService;
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(IChatService chatService, ILogger<ChatHub> logger)
    {
        _chatService = chatService;
        _logger = logger;
    }

    public async Task SendMessage(Guid recipientId, string message)
    {
        message = message.Trim();

        if (string.IsNullOrEmpty(message)) return;

        var userId = Context.User!.UserId();

        var roomId = _chatService.GetRoom(userId, recipientId);

        _logger.LogInformation("User {UserId} send message to {RecipientId} with room {roomId}", userId, recipientId,
            roomId);

        await Clients.GroupExcept(roomId, Context.ConnectionId).ReceiveMessage(message);
    }

    public async Task JoinChat(JoinChatRequest request)
    {
        var userId = Context.User!.UserId();

        var roomId = _chatService.GetRoom(userId, request.RecipientId);

        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

        await Clients.GroupExcept(roomId, Context.ConnectionId).ReceiveMessage("Подключился к чату");
    }
}

public interface IChatHub
{
    Task ReceiveMessage(string message);
}