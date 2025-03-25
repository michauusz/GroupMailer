using GroupMailer.Data;
using GroupMailer.Interfaces;
using GroupMailer.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupMailer.Repository.QueryRepository
{
    public class GroupQueryRepository : IGroupQueryRepository
    {
        private readonly AppDbContext _context;

        public GroupQueryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetContactsByGroupIdAsync(int groupId)
        {
            var result = await _context.Groups.Include(g => g.Contacts).FirstOrDefaultAsync(g => g.Id == groupId);

            return result.Contacts.ToList();
        }

        public async Task<List<string>> GetContactsEmailsByGroupIdAsync(int groupId)
        {
            var result = await _context.Groups.Include(g => g.Contacts).FirstOrDefaultAsync(g => g.Id == groupId);

            return result.Contacts.Select(e => e.Email).ToList();
        }

        public async Task<Group> GetGroupByIdAsync(int id)
        {
            return await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<Group>> GetGroupsByNameASync(string groupName)
        {
            return await _context.Groups.Where(g => g.Name == groupName).ToListAsync();
        }
    }
}
