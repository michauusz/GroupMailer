using GroupMailer.Models;

namespace GroupMailer.Interfaces
{
    public interface IGroupCommandRepository
    {
        Task AddGroup(Group group);
        Task DeleteGroup(Group group);
        Task AddContactToGroup(int contactId, int groupId);
        Task DeleteContactFromGroup(int contactId, int groupId);
    }
}
