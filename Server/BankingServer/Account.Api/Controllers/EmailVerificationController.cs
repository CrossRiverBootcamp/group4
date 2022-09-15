using Account.Services.Interfaces;
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
        [HttpPost]
        public async void PostEmailVerification([FromBody] string email)
        {
            try
            {
                await _emailVerificationService.AddEmailVerificationAsync(email);
            }
            catch(Exception ex)
            {
                throw new Exception("Couldn't verify email",ex);
            }
        }

        [HttpPost]
        public async void ResendCodeAsync([FromBody] string email)
        {
            try
            {
                await _emailVerificationService.ResendCodeAsync(email);
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't resend code", ex);
            }
        }

    }
}
