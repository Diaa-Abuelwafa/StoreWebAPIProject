using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.DTOs.AccountDTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Please Enter Your Display Name")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Please Enter Your UserName")]
        public string UserName { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmedPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
