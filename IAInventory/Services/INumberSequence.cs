using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAInventory.Services
{
    public interface INumberSequence
    {
        string GetNumberSequence(string module);
    }
}
