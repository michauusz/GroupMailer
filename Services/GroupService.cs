using GroupMailer.DTO;
using GroupMailer.Interfaces;
using GroupMailer.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GroupMailer.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupCommandRepository _commandRepository;
        private readonly IGroupQueryRepository _queryRepository;
        private readonly IUserQueryRepository _userQueryRepository;

        public GroupService(IGroupCommandRepository commandRepository, IGroupQueryRepository queryRepository, IUserQueryRepository userQueryRepository)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _userQueryRepository = userQueryRepository;
        }

        public async Task AddContactToGroup(int contactId, int groupId)
        {
            await _commandRepository.AddContactToGroup(contactId, groupId);
        }

        public async Task AddGroup(GroupDto group, string userEmail)
        {
            var user = await _userQueryRepository.GetByEmailAsync(userEmail);
            var newGroup = new Group
            {
                Name = group.Name,
                Description = group.Description,
                UserId = user.Id
            };
            await _commandRepository.AddGroup(newGroup);
        }

        public async Task DeleteContactFromGroup(int contactId, int groupId)
        {
            await _commandRepository.DeleteContactFromGroup(contactId, groupId);
        }

        public async Task DeleteGroup(Group group)
        {
            await _commandRepository.DeleteGroup(group);
        }

        public async Task<List<ContactUpdateDto>> GetContactsByGroupId(int groupId)
        {
            var contacts = await _queryRepository.GetContactsByGroupIdAsync(groupId);
            var contactsDto = new List<ContactUpdateDto>();
            foreach(var contact in contacts)
            {
                contactsDto.Add(new ContactUpdateDto
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    CustomName = contact.CustomName,
                    Email = contact.Email
                });
            }
            return contactsDto;
        }

        public async Task<Group> GetGroupById(int groupId)
        {
            return await _queryRepository.GetGroupByIdAsync(groupId);
        }

        public async Task<List<Group>> GetGroupsByName(string groupName)
        {
            return await _queryRepository.GetGroupsByNameASync(groupName);
        }
    }
}
