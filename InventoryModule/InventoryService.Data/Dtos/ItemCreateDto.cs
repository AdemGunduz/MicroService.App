using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Data.Dtos
{
    public record ItemCreateDto(string Name);
    public record ItemUpdateDto(string Id,string Name);
    
}
