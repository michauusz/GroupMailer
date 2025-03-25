using GroupMailer.Data;
using GroupMailer.Interfaces;
using GroupMailer.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupMailer.Repository.CommandRepository
{
    public class GroupCommandRepository : IGroupCommandRepository
    {
        private readonly AppDbContext _context;

        public GroupCommandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddContactToGroup(int contactId, int groupId)
        {
            await _context.Database.ExecuteSqlRawAsync("INSERT INTO ContactGroup (ContactsId, GroupsId) VALUES ({0}, {1})", contactId, groupId);
        }

        public async Task AddGroup(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactFromGroup(int contactId, int groupId)
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM ContactGroup WHERE ContactsId = {0} AND GroupsId = {1}", contactId, groupId);
        }

        public async Task DeleteGroup(Group group)
        {
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }
    }
}
