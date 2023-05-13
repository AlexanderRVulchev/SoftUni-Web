using Contacts.Models;

namespace Contacts.Contracts
{
    public interface IContactsService
    {
        Task<IEnumerable<ContactViewModel>> GetAllContactsAsync();
    }
}
