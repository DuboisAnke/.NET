using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PXLFunds.Data
{
    public class Fund
    {
        public int FundId { get; set; }
        public int BankId { get; set; }
        public string FundName { get; set; }
        public decimal FundValue { get; set; }
        public Bank Bank { get; set; }
        public ICollection<UserFund> UserFunds { get; set; }

    }
}
