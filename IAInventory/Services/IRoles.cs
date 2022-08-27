using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAInventory.Services
{
    public interface IRoles
    {
        Task GenerateRolesFromPagesAsync();

        Task AddToRoles(string applicationUserId);
    }
}
