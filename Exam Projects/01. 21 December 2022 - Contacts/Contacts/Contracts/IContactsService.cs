using Contacts.Models;

namespace Contacts.Contracts
{
    public interface IContactsService
    {
        Task<IEnumerable<ContactViewModel>> GetAllContactsAsync();

        Task AddNewContactAsync(ContactViewModel model);

        Task<ContactViewModel?> GetContactByIdAsync(int contactId);

        Task UpdateContactAsync(int contactId, ContactViewModel model);

        Task AddContactToUserCollectionAsync(string userId, int contactId);

        Task<IEnumerable<ContactViewModel>> GetUserTeamContactsAsync(string userId);

        Task RemoveContactFromUserCollectionAsync(string userId, int contactId);
    }
}
