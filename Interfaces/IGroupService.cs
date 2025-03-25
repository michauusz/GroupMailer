using GroupMailer.DTO;
using GroupMailer.Models;

namespace GroupMailer.Interfaces
{
    public interface IGroupService
    {
        Task<Group> GetGroupById(int groupId);
        Task<List<Group>> GetGroupsByName(string groupName);
        Task<List<ContactUpdateDto>> GetContactsByGroupId(int groupId);
        Task AddContactToGroup(int contactId, int groupId);
        Task DeleteContactFromGroup(int contactId, int groupId);
        Task DeleteGroup(Group group);
        Task AddGroup(GroupDto group, string userEmail);
    }
}
