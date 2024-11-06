using DomainStore.DTOs.AccountDTOs;
using DomainStore.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.Interfaces.Account
{
    public interface IAccountService
    {
        public Task<List<string>> Register(RegisterDTO User);
        public Task<LoginResponseDTO> Login(LoginDTO User);
        public UserResponse GetCurrentUser(string Id);
    }
}
