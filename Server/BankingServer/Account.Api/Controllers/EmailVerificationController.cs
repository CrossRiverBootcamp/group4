using Account.DTO;
using Account.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailVerificationController : ControllerBase
    {
        private readonly IEmailVerificationService _emailVerificationService;
       
        public EmailVerificationController(IEmailVerificationService emailVerificationService)
        {
            _emailVerificationService = emailVerificationService;
        }
    
        // POST api/<IEmailVerificationController>
        [HttpPost("{email}")]
        public async void PostEmailVerification(string email)
        {
            await _emailVerificationService.AddEmailVerification(email);
           
        }

    }
}
