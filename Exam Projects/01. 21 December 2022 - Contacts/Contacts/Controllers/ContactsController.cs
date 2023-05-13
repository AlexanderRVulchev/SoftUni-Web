using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    using Contracts;
    using Models;
    using System.Security.Claims;

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

            await service.AddNewContactAsync(model);
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contactModel = await service.GetContactByIdAsync(id);
            if (contactModel == null)
            {
                return NotFound();
            }
            return View(contactModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await service.UpdateContactAsync(id, model);
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> AddToTeam(int contactId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await service.AddContactToUserCollectionAsync(userId, contactId);
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Team()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var models = await service.GetUserTeamContactsAsync(userId);
            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromTeam(int contactId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await service.RemoveContactFromUserCollectionAsync(userId, contactId);
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Team));
        }
    }
}
