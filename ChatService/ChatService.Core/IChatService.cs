namespace ChatService.Core;

public interface IChatService
{
    string GetRoom(Guid userId, Guid recipientId);
}