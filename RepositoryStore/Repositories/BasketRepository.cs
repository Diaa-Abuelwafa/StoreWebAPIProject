using DomainStore.Interfaces;
using DomainStore.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepositoryStore.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        IDatabase Context;
        public BasketRepository(IConnectionMultiplexer Redis)
        {
            Context = Redis.GetDatabase();
        }
        public Basket GetBasket(string BasketId)
        {
            var Basket = Context.StringGet(BasketId);

            if(!string.IsNullOrEmpty(Basket))
            {
                var BasketDeserialized = JsonSerializer.Deserialize<Basket>(Basket);

                return BasketDeserialized;
            }

            return null;
        }
        public Basket AddOrUpdateBasket(Basket basket)
        {
            var BasketSerialized = JsonSerializer.Serialize(basket);

            bool Flag = Context.StringSet(basket.Id, BasketSerialized);

            if(Flag)
            {
                return GetBasket(basket.Id);
            }

            return null;
        }

        public bool DeleteBasket(string BasketId)
        {
            return Context.KeyDelete(BasketId);
        }
    }
}
