using GroupMailer.Models;

namespace GroupMailer.Interfaces
{
    public interface IUserQueryRepository
    {
        Task<bool> DoesUserExistByEmailAsync(string email);
        Task<User> GetByEmailAsync(string email);
    }
}
