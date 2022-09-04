using Account.DTO;
using Account.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountServise;
        public AccountController(IAccountService accountServise)
        {
            _accountServise = accountServise;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountInfoDTO>> GetAccountInfo(int id)
        {
            try
            {
                return await _accountServise.GetAccountInfo(id);
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<int>> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                return await _accountServise.Login(loginDTO);
            }
            catch(Exception ex)
            {
                return Unauthorized(ex);
            }
        }
    }
}
