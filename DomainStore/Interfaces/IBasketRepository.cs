using DomainStore.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.Interfaces
{
    public interface IBasketRepository
    {
        public Basket GetBasket(string BasketId);
        public Basket AddOrUpdateBasket(Basket basket);
        public bool DeleteBasket(string BasketId);
    }
}
