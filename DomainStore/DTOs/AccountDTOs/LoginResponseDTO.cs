using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.DTOs.AccountDTOs
{
    public class LoginResponseDTO
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
