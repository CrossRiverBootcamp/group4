using Account.DTO;
using Account.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IEmailVerificationService _emailVerificationService;
        //private readonly IConfiguration _configuration;
        private readonly IOptions<InitBalance> _options;
     
        
        public CustomerController(IAccountService accountService, IEmailVerificationService emailVerificationService, IOptions<InitBalance> options)
        {
            _accountService = accountService;
            _emailVerificationService = emailVerificationService;
            _options = options;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateAccountAsync([FromBody] CustomerDTO customer)
        {
            if (await _emailVerificationService.CheckVerificationAsync(customer.Email, customer.VerificationCode))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                try

                {
                    await _accountService.CreateAccountAsync(customer, _options.Value.Balance);
                    return Ok(true);
                }
                catch
                {
                    return Ok(false);
                }
            }
            else
            {
                throw new Exception("The verification code is wrong or expired. Can't create account");
            }
        }
        [HttpGet("{accountId}")]
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
