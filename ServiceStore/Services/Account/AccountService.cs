using DomainStore.DTOs.AccountDTOs;
using DomainStore.Identity;
using DomainStore.Interfaces.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceStore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStore.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly IConfiguration Config;

        public AccountService(UserManager<ApplicationUser> UserManager, IConfiguration Config)
        {
            this.UserManager = UserManager;
            this.Config = Config;
        }
        public async Task<List<string>> Register(RegisterDTO User)
        {
            List<string> Errors = new List<string>();

            ApplicationUser AppUser = new ApplicationUser()
            {
                DisplayName = User.DisplayName,
                UserName = User.UserName,
                Email = User.Email,
                AddressId = User.AddressId
            };

            ApplicationUser UserByEmail = await UserManager.FindByEmailAsync(User.Email);

            if(UserByEmail is not null)
            {
                Errors.Add("This Email Address Already Token");

                return Errors;
            }

            IdentityResult Result = await UserManager.CreateAsync(AppUser, User.Password);

            if(Result.Succeeded)
            {
                return Errors;
            }

            foreach(var Error in Result.Errors)
            {
                Errors.Add(Error.Description);
            }

            return Errors;
        }
        public async Task<LoginResponseDTO> Login(LoginDTO User)
        {
            ApplicationUser AppUser = await UserManager.FindByEmailAsync(User.Email);

            if(AppUser is not null)
            {
                LoginResponseDTO Response = new LoginResponseDTO();

                Response.DisplayName = AppUser.DisplayName;

                var TokenService = new JwtService(UserManager, Config);
                Response.Token = await TokenService.GenerateJwtToken(AppUser);

                Response.ExpireTime = DateTime.Now.AddMinutes(double.Parse(Config["Jwt:ExpireMinutes"]));

                return Response;
            }

            return null;
        }

        public UserResponse GetCurrentUser(string Id)
        {
            ApplicationUser UserFromDb = UserManager.FindByEmailWithId(Id);

            if(UserFromDb is null)
            {
                return null;
            }

            UserResponse UserResponse = new UserResponse()
            {
                Email = UserFromDb.Email,
            };

            if(UserFromDb.Address is not null)
            {
                UserResponse.AddressCity = UserFromDb.Address.City;
            }

            return UserResponse;
        }
    }
}
