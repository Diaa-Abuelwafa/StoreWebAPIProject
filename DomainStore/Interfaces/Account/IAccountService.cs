using DomainStore.DTOs.AccountDTOs;
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
    }
}
