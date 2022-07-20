using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PXLFunds.Data
{
    public class Bank
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public ICollection<Fund> Funds { get; set; }
    }
}
