using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PXLFunds.Services
{
    public interface ISeedDataRepository
    {
        Task Initialise();
    }
}
