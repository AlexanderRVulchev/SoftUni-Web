using Microsoft.EntityFrameworkCore;

namespace Contacts.Services
{
    using Contracts;
    using Data;
    using Data.Entities;
    using Models;

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
                    Id = c.Id,
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

        public async Task<ContactViewModel?> GetContactByIdAsync(int contactId)
        {
            var entity = await context.Contacts.FindAsync(contactId);

            if (entity != null)
            {
                ContactViewModel model = new()
                {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Address = entity.Address,
                    Email = entity.Email,
                    PhoneNumber = entity.PhoneNumber,
                    Website = entity.Website,
                    Id = entity.Id
                };
                return model;
            }
            return null;
        }

        public async Task UpdateContactAsync(int contactId, ContactViewModel model)
        {
            var entity = await context.Contacts.FindAsync(contactId);

            if (entity != null)
            {
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Address = model.Address;
                entity.PhoneNumber = model.PhoneNumber;
                entity.Email = model.Email;
                entity.Website = model.Website;
            }

            await context.SaveChangesAsync();
        }

        public async Task AddContactToUserCollectionAsync(string userId, int contactId)
        {
            var contactEntity = await context.Contacts.FindAsync(contactId);

            if (contactEntity != null
                && !contactEntity.ApplicationUsersContacts.Any(auc => auc.ApplicationUserId == userId))
            {
                contactEntity.ApplicationUsersContacts.Add(new ApplicationUserContact
                {
                    ApplicationUserId = userId
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ContactViewModel>> GetUserTeamContacts(string userId)
        {
            var entities = await context.ApplicationUsersContacts
                .Where(auc => auc.ApplicationUserId == userId)
                .Select(auc => auc.Contact)
                .ToArrayAsync();

            var models = entities
                .Select(e => new ContactViewModel
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Address = e.Address,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    Website = e.Website
                })
                .ToArray();
                
            return models;
        }
    }
}

