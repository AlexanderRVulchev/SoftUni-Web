using Contacts.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactsService service;

        public ContactsController(IContactsService _service)
        {
            this.service = _service;
        }

        public async Task<IActionResult> All()
        {
            var models = await service.GetAllContactsAsync();

            return View(models);
        }
    }
}
