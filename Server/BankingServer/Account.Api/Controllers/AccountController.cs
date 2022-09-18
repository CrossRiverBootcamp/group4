using Account.DTO;
using Account.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountInfoDTO>> GetAccountInfoAsync(int id)
        {
            try
            {
                return await _accountService.GetAccountInfoAsync(id);
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<int>> LoginAsync([FromBody] LoginDTO loginDTO)
        {
            try
            {
                return await _accountService.LoginAsync(loginDTO);
            }
            catch(Exception ex)
            {
                return Unauthorized(ex);
            }
        }
    }
}
