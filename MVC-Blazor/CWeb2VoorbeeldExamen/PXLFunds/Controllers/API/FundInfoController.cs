using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PXLFunds.Data;
using PXLFunds.Services;

namespace PXLFunds.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundInfoController : ControllerBase
    {
        private readonly IFundRepository _fundRepository;

        public FundInfoController(IFundRepository fundRepository)
        {
            _fundRepository = fundRepository;
        }

        [HttpGet("{bankName}")]
        public async Task<IActionResult> GetFundsForBank(string bankName)
        {
            return Ok(_fundRepository.GetByNameAsync(bankName));

        }

    }
}
