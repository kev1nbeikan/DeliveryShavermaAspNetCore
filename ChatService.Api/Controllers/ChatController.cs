using Microsoft.AspNetCore.Mvc;

namespace ChatService.Api.Controllers;

public class ChatController: Controller
{
    public IActionResult Room()
    {
        return View();
    }
    
}