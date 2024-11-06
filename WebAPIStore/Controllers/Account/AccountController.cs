using DomainStore.DTOs.AccountDTOs;
using DomainStore.Interfaces.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPIStore.Helpers;

namespace WebAPIStore.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService AccountService;

        public AccountController(IAccountService AccountService)
        {
            this.AccountService = AccountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO UserData)
        {
            List<string> RegisterErrors = await AccountService.Register(UserData);

            if(RegisterErrors.Count() == 0)
            {
                return Created();
            }

            return BadRequest(new ApiValidationErrorResponse((int)HttpStatusCode.BadRequest, errors: RegisterErrors));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO UserData)
        {
            LoginResponseDTO Response = await AccountService.Login(UserData);

            if(Response is not null)
            {
                return Ok(Response);
            }

            return Unauthorized(new ApiErrorResponse((int)HttpStatusCode.Unauthorized, "Invalid Username Or Password"));
        }
    }
}
