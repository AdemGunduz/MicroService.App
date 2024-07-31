using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Data.Dtos
{
    public record ItemInventoryCreateDto (string InventoryId, string ItemId, int Count);
    public record ItemInventoryUpdateDto (string Id ,string InventoryId, string ItemId, int Count);

}
