using Account.Services.Interfaces;
using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.NSB
{
    public class TransactionPayloadHandler : IHandleMessages<TransactionPayload>
    {
        private readonly IAccountSagaService _accountSagaService;
        static ILog log = LogManager.GetLogger<TransactionPayloadHandler>();
        static BalanceUpdated balanceUpdated = new();
        public TransactionPayloadHandler(IAccountSagaService accountSagaService)
        {
            _accountSagaService = accountSagaService;
        }
        
        public async Task Handle(TransactionPayload message, IMessageHandlerContext context)
        {
            if (await _accountSagaService.CheckIdValid(message.FromAccount) && await _accountSagaService.CheckIdValid(message.ToAccount)
                && await _accountSagaService.CheckBalance(message.FromAccount, message.Amount))
            {
                try
                {
                    await _accountSagaService.UpdateBalance(message.FromAccount, message.ToAccount, message.Amount);
                    balanceUpdated.BalanceUpdatedSucceeded = true;
                }
                catch
                {
                    balanceUpdated.BalanceUpdatedSucceeded = false;
                }

            }
            else
            {
                balanceUpdated.BalanceUpdatedSucceeded = false;
            }
            await context.Publish(balanceUpdated);
        }
    }
}
