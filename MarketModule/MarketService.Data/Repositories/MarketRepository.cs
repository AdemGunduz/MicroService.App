using MarketService.Data.Contexts;
using MarketService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Data.Repositories
{
    public class MarketRepository : IMarketRepository
    {
        private readonly MarketContext _marketContext;
        private readonly StackExchange.Redis.IDatabase _redisDatabase;

        public MarketRepository(MarketContext marketContext, IConnectionMultiplexer redisConnection)
        {
            _marketContext = marketContext;
            _redisDatabase = redisConnection.GetDatabase();
        }

        public Market Add(Market market)
        {
            _marketContext.Markets.Add(market);
            _marketContext.SaveChanges();
            return market;
        }

        public List<Market> GetAll()
        {
            return _marketContext.Markets.AsNoTracking().ToList();
        }

        
    }
}
