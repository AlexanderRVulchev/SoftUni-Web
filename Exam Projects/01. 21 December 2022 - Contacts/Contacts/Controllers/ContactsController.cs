using Contacts.Contracts;
using Contacts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [Authorize]
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

        [HttpGet]
        public IActionResult Add()
        {
            ContactViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await service.AddNewContact(model);
            return RedirectToAction(nameof(All));
        }
    }
}
