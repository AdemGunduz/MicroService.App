using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketService.Data.Dtos
{
    public record MarketDtos(string ItemId, string InventoryId, decimal Price, string PlayerId);

}
