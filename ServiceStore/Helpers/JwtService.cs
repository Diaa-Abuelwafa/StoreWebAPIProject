using DomainStore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStore.Helpers
{
    public class JwtService
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly IConfiguration Config;
        public JwtService(UserManager<ApplicationUser> UserManager, IConfiguration Config)
        {
            this.UserManager = UserManager;
            this.Config = Config;
        }
        public async Task<string> GenerateJwtToken(ApplicationUser User)
        {
            List<Claim> Claims = new List<Claim>();

            Claims.Add(new Claim(ClaimTypes.NameIdentifier, User.Id));
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, User.UserName));

            var Roles = await UserManager.GetRolesAsync(User);
            foreach(var RoleName in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, RoleName));
            }

            string UniqueToken = Guid.NewGuid().ToString();
            Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, UniqueToken));

            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:SecretKey"]));
            SigningCredentials Credentials = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken Token = new JwtSecurityToken(
                issuer: Config["Jwt:ProviderBaseUrl"],
                audience: Config["Jwt:ConsumerBaseUrl"],
                expires: DateTime.Now.AddMinutes(double.Parse(Config["Jwt:ExpireMinutes"])),
                claims: Claims,
                signingCredentials: Credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
