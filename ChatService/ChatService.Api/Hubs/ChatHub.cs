using BarsGroupProjectN1.Core.Extensions;
using ChatService.Core;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Api.Hubs;

public class ChatHub : Hub<IChatHub>
{
    private readonly IChatService _chatService;

    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task SendMessage(string user, string message, Guid recipientId)
    {
        var userId = Context.User!.UserId();

        var roomId = _chatService.GetRoom(userId, recipientId);

        await Clients.Group(roomId).ReceiveMessage(user, message);
    }
}

public interface IChatHub
{
    Task ReceiveMessage(string user, string message);
}