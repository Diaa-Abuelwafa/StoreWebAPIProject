using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.Interfaces.Caching
{
    public interface ICachService
    {
        public string GetCachData(string Key);
        public bool SetCachData(string Key, object Value, TimeSpan ExpireTime);
    }
}
