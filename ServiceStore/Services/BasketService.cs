using DomainStore.DTOs.BasketDTOs;
using DomainStore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainStore.Models.BasketModule;

namespace ServiceStore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository BasketRepository;

        public BasketService(IBasketRepository BasketRepository)
        {
            this.BasketRepository = BasketRepository;
        }
        public BasketDTO GetBasket(string BasketId)
        {
            Basket basket = BasketRepository.GetBasket(BasketId);

            if(basket is not null)
            {
                // Mapping To BasketDTO
                BasketDTO basketDTO = new BasketDTO();
                basketDTO.Id = basket.Id;
                basketDTO.Items = basket.Items;

                return basketDTO;
            }

            return null;
        }
        public BasketDTO AddOrUpdateBasket(BasketDTO Basket)
        {
            // Mapping To Basket
            Basket basket = new Basket();
            basket.Id = Basket.Id;
            basket.Items = Basket.Items;
            BasketRepository.AddOrUpdateBasket(basket);

            BasketDTO BasketResponse = GetBasket(basket.Id);

            return BasketResponse;
        }

        public bool DeleteBasket(string BasketId)
        {
            return BasketRepository.DeleteBasket(BasketId);
        }
    }
}
