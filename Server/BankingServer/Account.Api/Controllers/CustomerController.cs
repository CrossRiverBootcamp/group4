﻿using Account.DTO;
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
        private readonly IOptions<InitBalance> _options;


        public CustomerController(IAccountService accountService, 
            IEmailVerificationService emailVerificationService, IOptions<InitBalance> options)
        {
            _accountService = accountService;
            _emailVerificationService = emailVerificationService;
            _options = options;
        }


        //create new account
        [HttpPost("CreateAccount")]
        public async Task<ActionResult<int>> CreateAccountAsync([FromBody] CustomerDTO customer)
        {
            //check verification 

            try
            {
                if (await _emailVerificationService.CheckVerificationAsync(customer.Email, customer.VerificationCode))
                {
                    // create account with option value : 100 
                    int accountId = await _accountService.CreateAccountAsync(customer, _options.Value.Balance);
                    return Ok(accountId);
                }
                else
                {
                    return Unauthorized("The verification code is wrong or expired. Can't create account");
                }
            }
            catch(Exception ex)
            {
                return Unauthorized(ex);
            }
        }

    //get customer by account id for transaction details
    [HttpGet("{accountId}")]
    public async Task<ActionResult<CustomerDTO>> GetCustomerByAccountId(int accountId)
    {
        try
        {
            return await _accountService.GetCustomerByAccountId(accountId);
        }
        catch (Exception ex)
            {
            return NotFound(ex);
        }
    }

}
}
