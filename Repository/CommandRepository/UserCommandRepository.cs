using GroupMailer.Data;
using GroupMailer.Interfaces;
using GroupMailer.Models;

namespace GroupMailer.Repository.CommandRepository
{
    public class UserCommandRepository : IUserCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(User user)
        {
            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
