using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ServiceLB.Models;
using ServiceLB.StaticModels;

namespace ServiceLB
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateAccessToken(Auth auth)
        {
            if (_configuration["Email"] != auth.Email || _configuration["Pass"] != auth.Password)
            {
                return "User or password not match";
            }

            return await GenerateJSONWebToken(auth).ConfigureAwait(false);
        }

        private async Task<string> GenerateJSONWebToken(Auth auth)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Email, auth.Email ),
                    new Claim(ClaimTypes.Email, auth.Email),
                    new Claim(ClaimTypes.Role, nameof(Role.Admin) ),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var token = new JwtSecurityToken(
              _configuration["JWT:Issuer"],
              _configuration["JWT:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWT:ExpireMin"])),
              signingCredentials: credentials);

            return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token)).ConfigureAwait(false);
        }
    }
}