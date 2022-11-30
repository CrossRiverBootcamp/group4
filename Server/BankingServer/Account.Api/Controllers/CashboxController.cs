using Account.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Account.Services.Interfaces;


namespace Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashboxController : ControllerBase
    {
        private readonly ICashboxService _cashboxService;
        public CashboxController(ICashboxService cashboxService)
        {
            _cashboxService = cashboxService;
        }
        [HttpPost]
        public async Task<ActionResult<int>> PostCashbox([FromBody] CashboxDTO cashboxDTO)
        {
            try
            {
               return Ok(await _cashboxService.CreateCashboxAsync(cashboxDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("details/{accountId}")]
        public async Task<ActionResult<CashboxDTO>> GetCashboxAsync(int accountId)
        {
            try
            {
                return Ok(await _cashboxService.GetCashboxAsync(accountId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("exist/{accountId}")]
        public async Task<ActionResult<bool>> CheckCashboxExists(int accountId)
        {
            try
            {
                return Ok(await _cashboxService.CheckCashboxExists(accountId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut]
        public void UpdateCahboxAsync(int accountId, [FromBody] CashboxDTO cashboxDTO)
        {
            try
            {
                _cashboxService.UpdateCahboxAsync(accountId, cashboxDTO);
            }
            catch(Exception ex)
            {
                BadRequest(ex);
            }
        }
    }
}
