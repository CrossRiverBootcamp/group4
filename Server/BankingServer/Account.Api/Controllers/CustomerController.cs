using Account.DTO;
using Account.Services.Interfaces;
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
        public async Task<ActionResult<bool>> CreateAccountAsync([FromBody] CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
               await _accountService.CreateAccountAsync(customer);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerByAccountId(int accountId)
        {
            try
            {
                return await _accountService.GetCustomerByAccountId(accountId);
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
