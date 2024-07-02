namespace ChatService.Api.Contracts;

public class RoomRequest
{
    public Guid RecipientId { get; set; }
    public string ChatName { get; set; }
}