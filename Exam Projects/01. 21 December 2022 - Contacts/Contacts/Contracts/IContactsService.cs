using Contacts.Models;

namespace Contacts.Contracts
{
    public interface IContactsService
    {
        Task<IEnumerable<ContactViewModel>> GetAllContactsAsync();

        Task AddNewContact(ContactViewModel model);

        Task<ContactViewModel?> GetContactByIdAsync(int contactId);

        Task UpdateContactAsync(int contactId, ContactViewModel model);
    }
}
