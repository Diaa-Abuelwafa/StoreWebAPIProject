using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPIStore.Helpers;

namespace WebAPIStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error()
        {
            return NotFound(new ApiErrorResponse((int)HttpStatusCode.NotFound, "Not Found EndPoint"));
        }
    }
}
