using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private static List<KeyValuePair<string, string>> Messages = new();

        public IActionResult Show()
        {
            if (!Messages.Any())
            {
                return View(new ChatViewModel());
            }

            var chatModel = new ChatViewModel()
            {
                Messages = Messages
                    .Select(m => new MessageViewModel
                    {
                        Sender = m.Key,
                        MessageText = m.Value
                    })
                    .ToList()
            };

            return View(chatModel);
        }

        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            Messages.Add(new(chat.CurrentMessage.Sender, chat.CurrentMessage.MessageText));
            return RedirectToAction(nameof(Show));
        }
    }
}
