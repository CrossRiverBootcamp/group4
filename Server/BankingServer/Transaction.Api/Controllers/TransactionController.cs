using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using Transaction.DTO;
using Transaction.Services.Interfaces;

namespace Transaction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMessageSession _messageSession;
        public TransactionController(ITransactionService transactionService, IMessageSession messageSession)
        {
            _transactionService = transactionService;
            _messageSession = messageSession;
        }
        [HttpPost]
        public async Task<ActionResult> CreateTransaction(TransactionDto transactionDto)
        {
            try
            {
                var result = _transactionService.SendTransaction(transactionDto, _messageSession);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }
    }
}
