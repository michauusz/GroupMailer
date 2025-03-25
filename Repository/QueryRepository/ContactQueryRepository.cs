using GroupMailer.Data;
using GroupMailer.Interfaces;
using GroupMailer.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupMailer.Repository.QueryRepository
{
    public class ContactQueryRepository : IContactQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public ContactQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Contact> GetContactByIdAsync(int id) => await _appDbContext.Contacts.FirstOrDefaultAsync(i => i.Id == id);

        public async Task<List<Contact>> GetAllUserContactAsync(int userId) => await _appDbContext.Contacts.Where(c => c.Users.Any(u => u.Id == userId)).ToListAsync();

        public async Task<List<Contact>> GetAllAsync() => await _appDbContext.Contacts.ToListAsync();

        public async Task<List<Contact>> GetAllOtherCOntactAsync(int userId) => await _appDbContext.Contacts.Where(c => c.Users.Any(u => u.Id != userId)).ToListAsync();
    }
}
