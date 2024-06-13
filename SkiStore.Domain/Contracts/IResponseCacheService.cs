using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Domain.Contracts
{
    public interface IResponseCacheService
    {
       Task CashResponseAsync(string cacheKey,object response,TimeSpan timeToLive);
        Task<string> GetCacheResponseAsync(string cacheKey);
    }
}
