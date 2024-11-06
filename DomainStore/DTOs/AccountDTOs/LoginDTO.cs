using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.DTOs.AccountDTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Please Enter Your EmailAddress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        public string Password { get; set; }
    }
}
