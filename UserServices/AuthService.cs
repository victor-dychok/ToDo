using Common.BL.Exeptions;
using Common.Domain;
using Common.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserServices.dto;
using UserServices.Utils;

namespace UserServices
{
    public class AuthService : IAutnService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IRepository<User> users, IConfiguration configuration) 
        {
            _userRepository = users;
            _configuration = configuration;
        }
        public async Task<string> GetJwtTokenAsync(AuthDto auth)
        {
            var user = await _userRepository.SingleOrDefaultAsync(u => u.Login == auth.Login.Trim());
            if(user is null)
            {
                throw new Common.BL.NotFoundExeption($"No user with login: {auth.Login}");
            }

            if(!PasswordHashUtil.Verify(auth.Password, user.PasswordHash))
            {
                throw new ForbidenExeption("Incorrect authorization data");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, auth.Login),
                new(ClaimTypes.NameIdentifier, auth.Id.ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDeskription = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDeskription)!;

            return token;
        }
    }
}
