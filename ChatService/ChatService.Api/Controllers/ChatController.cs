using Microsoft.AspNetCore.Mvc;

namespace ChatService.Api.Controllers;

public class ChatController : Controller
{
    public IActionResult Room(Guid recipientId)
    {
        return View(new RoomViewModel
        {
            RecipientId = recipientId
        });
    }
}