using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Data.Dtos
{
    public record InventoryCreateDto(string PlayerId, string Name);
    public record InventoryUpdateDto(string Id,string PlayerId, string Name);
  
}
