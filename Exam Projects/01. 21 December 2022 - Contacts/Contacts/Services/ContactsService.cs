using Contacts.Contracts;
using Contacts.Data;
using Contacts.Data.Entities;
using Contacts.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Services
{
    public class ContactsService : IContactsService
    {
        private readonly ContactsDbContext context;

        public ContactsService(ContactsDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IEnumerable<ContactViewModel>> GetAllContactsAsync()
        {
            var contactEntities = await context.Contacts.ToArrayAsync();
            
            var contactModels = contactEntities
                .Select(c => new ContactViewModel
                {
                    ContactId = c.Id,
                    Address = c.Address,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    Website = c.Website,
                })
                .ToArray();

            return contactModels;
        }

        public async Task AddNewContact(ContactViewModel model)
        {
            Contact entity = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Website = model.Website
            };

            await context.Contacts.AddAsync(entity);
            await context.SaveChangesAsync();
        }
    }
}
