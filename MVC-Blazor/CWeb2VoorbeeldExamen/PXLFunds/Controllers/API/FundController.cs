using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PXLFunds.Data;
using PXLFunds.Models;
using PXLFunds.Services;

namespace PXLFunds.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundController : ControllerBase
    {
        private readonly IFundRepository _fundRepository;

        public FundController(IFundRepository fundRepository)
        {
            _fundRepository = fundRepository;
        }



        [HttpPost]
        public IActionResult AddFund(FundInfo fundInfo)
        {
            _fundRepository.AddFund(fundInfo);
            return Ok();

        }
    }
}
