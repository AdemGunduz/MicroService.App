using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IMarketCreated
    {
        public string ItemId { get; set; }
        public string InventoryId { get; set; }
        public int Count { get; set; }
    }
}
