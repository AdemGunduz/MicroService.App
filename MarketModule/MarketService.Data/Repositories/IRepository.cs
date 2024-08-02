using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketService.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        List<T> GetAll();
    }
}
