using GroupMailer.Data;
using GroupMailer.Interfaces;
using GroupMailer.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupMailer.Repository.QueryRepository
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly AppDbContext _context;

        public UserQueryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesUserExistByEmailAsync(string email)
        {
            var exist =  await _context.Users.FirstOrDefaultAsync(e => e.Email == email);
            return exist != null;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
