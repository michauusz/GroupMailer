using GroupMailer.Models;

namespace GroupMailer.Interfaces
{
    public interface IUserCommandRepository
    {
        Task AddAsync(User user);
    }
}
