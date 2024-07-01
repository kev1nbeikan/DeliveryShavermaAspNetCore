using BarsGroupProjectN1.Core.Extensions;
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

    public async Task SendMessage(string user, string message)
    {
        var userId = Context.User!.UserId();

        _logger.LogInformation("User {UserId} send message", userId);
        
        // var roomId = _chatService.GetRoom(userId, recipientId);

        // await Clients.Group(roomId).ReceiveMessage(user, message);
    }
}

public interface IChatHub
{
    Task ReceiveMessage(string user, string message);
}