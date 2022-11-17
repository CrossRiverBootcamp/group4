using Account.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashboxController : ControllerBase
    {
        [HttpPost]
        public async Task PostCashbox([FromBody] CashboxDTO cashboxDTO)
        {
            try
            {
                //await _emailVerificationService.AddEmailVerificationAsync(email);
            }
            catch (Exception ex)
            {
                //throw new Exception("Couldn't verify email", ex);
            }
        }
    }
}
