using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Data.Dtos
{
    public record PlayerCreateDto (string FirstName, string LastName, string UserName);
    public record PlayerUpdateDto (string Id,string FirstName, string LastName, string UserName);
    
}
