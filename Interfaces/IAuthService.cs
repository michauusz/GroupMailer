using GroupMailer.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace GroupMailer.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email);
        Task<string> RegisterAsync(RegisterRequest request);
        Task<string> LoginAsync(LoginRequest request);
        
    }
}
