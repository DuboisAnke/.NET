using System.Collections.Generic;
using System.Threading.Tasks;
using PXLFunds.Data;
using PXLFunds.Models;

namespace PXLFunds.Services
{
    public interface IFundRepository
    {
        Task <List<FundInfo>> GetByNameAsync(string name);
        void AddFund(FundInfo fundInfo);
        void AddUserFund(UserFund userFund);
        IEnumerable<UserFund> GetAllUserFunds();
    }
}
