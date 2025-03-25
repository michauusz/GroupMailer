using GroupMailer.DTO;
using GroupMailer.Models;

namespace GroupMailer.Interfaces
{
    public interface IContactService
    {
        Task<string> AddNewContactAsync(Contact contact, string email);
        Task<string> AddExistingContactAsync(int userId, int contactId);
        Task<bool> DeleteContactAsync(int userId, int contactId);
        Task<string> ModifyConctactAsync(ContactUpdateDto contactDto);
        Task<Contact> GetContactByIdAsync(int UserId);
        Task<List<Contact>> GetAllUserContactAsync(int UserId);
        Task<List<Contact>> GetAllOtherCOntactAsync(int UserId);
        Task<List<Contact>> GetAllAsync();

    }
}
