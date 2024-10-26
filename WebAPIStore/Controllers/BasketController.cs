using DomainStore.DTOs.BasketDTOs;
using DomainStore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStore.Services;
using System.Net;
using WebAPIStore.Helpers;

namespace WebAPIStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService BasketService;

        public BasketController(IBasketService BasketService)
        {
            this.BasketService = BasketService;
        }

        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(typeof(BasketDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.NotFound)]
        public IActionResult GetBasket(string Id)
        {
            BasketDTO Basket = BasketService.GetBasket(Id);

            if(Basket is not null)
            {
                return Ok(Basket);
            }

            return NotFound(new ApiErrorResponse((int)HttpStatusCode.NotFound));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateOrUpdateBasket(BasketDTO Basket)
        {
            BasketDTO BaskerCreated = BasketService.AddOrUpdateBasket(Basket);

            return Created(nameof(GetBasket), BaskerCreated);
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.NotFound)]
        public IActionResult DeleteBasket(string Id)
        {
            bool Flag = BasketService.DeleteBasket(Id);

            if(Flag)
            {
                return NoContent();
            }

            return NotFound(new ApiErrorResponse((int)HttpStatusCode.NotFound));
        }
    }
}
