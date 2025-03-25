using GroupMailer.Data;
using GroupMailer.Interfaces;
using GroupMailer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace GroupMailer.Repository.CommandRepository
{
    public class ContactCommandRepository : IContactCommandRepository
    {
        private readonly AppDbContext _context;

        public ContactCommandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddContactAsync(int userId, int contactId)
        {
            await _context.Database.ExecuteSqlRawAsync("INSERT INTO ContactUser (ContactsId, UsersId) VALUES ({0}, {1})", contactId, userId);
        }

        public async Task AddNewContactAsync(Contact contact, int userId)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            await _context.Database.ExecuteSqlRawAsync("INSERT INTO ContactUser (ContactsId, UsersId) VALUES ({0}, {1})", contact.Id, userId);
        }

        public async Task<int> DeleteContactUserByUserIdAndContactIdAsync(int userId, int contactId)
        {
           return await _context.Database.ExecuteSqlRawAsync("DELETE FROM ContactUser WHERE UsersId = {0} AND ContactsId = {1}", userId, contactId);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }
    }
}
