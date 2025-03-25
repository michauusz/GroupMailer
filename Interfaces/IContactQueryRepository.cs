using GroupMailer.Models;

namespace GroupMailer.Interfaces
{
    public interface IContactQueryRepository
    {
        Task<Contact> GetContactByIdAsync(int id);
        Task<List<Contact>> GetAllUserContactAsync(int userId);
        Task<List<Contact>> GetAllAsync();
        Task<List<Contact>> GetAllOtherCOntactAsync(int userId);
    }
}
