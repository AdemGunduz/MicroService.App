using MarketService.Data.Contexts;
using MarketService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketService.Data.Repositories
{
    public class MarketRepository :IMarketRepository
    {
        private readonly MarketContext _marketContext;

        public MarketRepository(MarketContext marketContext)
        {
            _marketContext = marketContext;
        }

        public Market Add (Market market)
        {
            _marketContext.Markets.Add(market);
            _marketContext.SaveChanges();
            return market;
        }

        public List<Market> GetAll ()
        {
            return _marketContext.Markets.AsNoTracking().ToList();
        }

    }
}
