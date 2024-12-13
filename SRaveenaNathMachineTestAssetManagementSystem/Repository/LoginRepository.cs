using System.Security.Cryptography;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SRaveenaNathMachineTestAssetManagementSystem.Model;

namespace SRaveenaNathMachineTestAssetManagementSystem.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly MachineTestDbContext _context;
        private readonly IConfiguration _configuration;

        public LoginRepository(MachineTestDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            // Find the user in the database (no need to hash the incoming password here)
            var user = await _context.Users
                .Include(u => u.Role)  // Include the Role data using navigation property
                .FirstOrDefaultAsync(u => u.Username == username);

            // If user not found or credentials are invalid, return null
            if (user == null || user.Password != password)
                return null;

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.RoleName) // Include the role in the claims
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Set the token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); // Return the token as a string
        }
    }
}
