using Account.DTO;
using Account.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public CustomerController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateAccount([FromBody] CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
               await _accountService.CreateAccount(customer);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }
        }
    }
}
