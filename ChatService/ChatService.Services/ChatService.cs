using System.Security.Cryptography;
using ChatService.Core;

namespace ChatService.Services;

public class ChatService : IChatService
{
    public string GetRoom(Guid userId, Guid recipientId)
    {
        return GenerateRoomId(userId, recipientId);
    }


    private string GenerateRoomId(Guid userId, Guid recipientId)
    {
        var combinedGuid = userId.ToString() +
                           recipientId.ToString();

        using SHA256 sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(combinedGuid));

        var roomId = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

        return roomId;
    }
}