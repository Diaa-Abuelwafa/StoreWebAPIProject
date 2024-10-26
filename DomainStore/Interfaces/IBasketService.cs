using DomainStore.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.Interfaces
{
    public interface IBasketService
    {
        public BasketDTO GetBasket(string BasketId);
        public BasketDTO AddOrUpdateBasket(BasketDTO Basket);
        public bool DeleteBasket(string BasketId);
    }
}
