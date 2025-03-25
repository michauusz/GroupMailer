using GroupMailer.Models;

namespace GroupMailer.Interfaces
{
    public interface IGroupQueryRepository
    {
        Task<Group> GetGroupByIdAsync(int id);
        Task<List<Group>> GetGroupsByNameASync(string groupName);
        Task<List<Contact>> GetContactsByGroupIdAsync(int groupId);
        Task<List<string>> GetContactsEmailsByGroupIdAsync(int groupId);
    }
}
