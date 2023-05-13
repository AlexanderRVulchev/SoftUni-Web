using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    using Contacts.Data;
    using Contracts;
    using Models;

    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IContactsService service;
        private readonly ContactsDbContext context;

        public ContactsController(IContactsService _service, ContactsDbContext context)
        {
            this.service = _service;
            this.context = context;
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
                ModelState.AddModelError("", "Edit failed");
                return View(model);
            }

            return RedirectToAction(nameof(All));
        }
        //[HttpPost]
        //public IActionResult Edit(int id, ContactViewModel contactModel)
        //{
        //    var contact = context.Contacts.Find(id);
        //    if (contact == null)
        //    {
        //        return BadRequest();
        //    }

        //    contact.FirstName = contactModel.FirstName;
        //    contact.LastName = contactModel.LastName;
        //    contact.Address = contactModel.Address;
        //    contact.PhoneNumber = contactModel.PhoneNumber;
        //    contact.Website = contactModel.Website;
        //    contact.Email = contactModel.Email;

        //    context.SaveChanges();
        //    return RedirectToAction("All", "Contacts");
        //}
    }
}
