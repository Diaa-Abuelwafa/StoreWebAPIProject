using DomainStore.Interfaces.Caching;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceStore.Services.Chacing
{
    public class CachService : ICachService
    {
        private readonly IDatabase Redis;
        public CachService(IConnectionMultiplexer RedisConnection)
        {
            this.Redis = RedisConnection.GetDatabase();
        }
        public string GetCachData(string Key)
        {
            var Data = Redis.StringGet(Key);

            if(!string.IsNullOrEmpty(Data))
            {
                return Data;
            }

            return null;
        }

        public bool SetCachData(string Key, object Value, TimeSpan ExpireTime)
        {
            var SerializeOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var Data = JsonSerializer.Serialize(Value, SerializeOptions);

            return Redis.StringSet(Key, Data, ExpireTime);
        }
    }
}
