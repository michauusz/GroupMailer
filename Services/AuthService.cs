using GroupMailer.Interfaces;
using GroupMailer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace GroupMailer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IConfiguration configuration, IUserCommandRepository userCommandRepository, IUserQueryRepository userQueryRepository, IPasswordHasher passwordHasher)
        {
            _configuration = configuration;
            _userCommandRepository = userCommandRepository;
            _userQueryRepository = userQueryRepository;
            _passwordHasher = passwordHasher;
        }

        public string GenerateJwtToken(string email)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"]!)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await _userQueryRepository.GetByEmailAsync(request.Email); //powinno być DTO

            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user.HashPassword, request.Password);

            if (result == PasswordVerificationResult.Success)
                return "Login Succesfull";
            else return null;
        }

        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            if (await _userQueryRepository.DoesUserExistByEmailAsync(request.Email))
                return null;
            
            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var user = new User
            {
                Email = request.Email,
                HashPassword = hashedPassword
            };

            await _userCommandRepository.AddAsync(user);

            return "Account created";
        }

        
    }
}
