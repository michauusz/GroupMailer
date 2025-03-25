using GroupMailer.Models;

namespace GroupMailer.Interfaces
{
    public interface IContactCommandRepository
    {
        Task<int> DeleteContactUserByUserIdAndContactIdAsync(int userId, int contactId);
        Task AddNewContactAsync(Contact contact, int userId);
        Task AddContactAsync(int userId, int contactId);
        Task UpdateContactAsync(Contact contact);

    }
}
