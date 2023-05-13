using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
