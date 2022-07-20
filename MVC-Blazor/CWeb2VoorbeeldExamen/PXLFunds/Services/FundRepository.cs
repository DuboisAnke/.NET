using System.Threading;
using PXLFunds.Data;
using PXLFunds.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PXLFunds.Services
{
    public class FundRepository : IFundRepository
    {
        private readonly ApplicationDbContext _context;

        public FundRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public void AddFund(FundInfo fundInfo)
        {
            Fund fund = new Fund
            {
                FundName = fundInfo.FundName,
                FundValue = fundInfo.FundValue,
                Bank = _context.Bank.FirstOrDefault(x => x.BankName.Equals("KBC"))
            };

            _context.Fund.Add(fund);
            _context.SaveChangesAsync();

   

        }

        public void AddUserFund(UserFund userFund)
        {
            var identity = _context.Users.FirstOrDefault(x => x.Id == userFund.UserId);
            userFund.Identity = identity;

            var fund = _context.Fund.FirstOrDefault(x => x.FundId == userFund.FundId);
            userFund.Fund = fund;

            _context.UserFund.Add(userFund);
            _context.SaveChangesAsync();
        }

        public IEnumerable<UserFund> GetAllUserFunds()
        {
            return _context.UserFund.Include(p => p.Fund).Include(p => p.Identity).ToList();
        }

        public async Task<List<FundInfo>> GetByNameAsync(string name)
        {
            var data = _context.Fund.Include(x => x.Bank)
                .Where(x => x.Bank.BankName.ToUpper().Equals(name.ToUpper()));
            List<FundInfo> listOfFunds = new List<FundInfo>();
            foreach (var fund in data)
            {
                listOfFunds.Add(new FundInfo
                {
                    FundName = fund.FundName,
                    FundValue = fund.FundValue,
                    BankName = fund.Bank.BankName
                });
            }

            return listOfFunds;
        }

        
    }
}
