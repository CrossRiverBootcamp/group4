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
        public async Task<ActionResult<AccountInfoDTO>> GetAccountInfo(int id)
        {
            try
            {
                return await _accountService.GetAccountInfo(id);
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                return await _accountService.Login(loginDTO);
            }
            catch(Exception ex)
            {
                return Unauthorized(ex);
            }
        }
    }
}
