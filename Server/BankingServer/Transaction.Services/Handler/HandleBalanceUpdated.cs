using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Services.Interfaces;

namespace Transaction.Services.Handler
{
    public class HandleBalanceUpdated : IHandleMessages<BalanceUpdated>
    {
        private readonly IUpdateTransactionStatusService _transaction;
        public HandleBalanceUpdated(IUpdateTransactionStatusService transaction)
        {
            _transaction = transaction;
        }

        static ILog log = LogManager.GetLogger<HandleBalanceUpdated>();
        public async Task Handle(BalanceUpdated message, IMessageHandlerContext context)
        {
            log.Info($"Received BalanceUpdated , did I have success?  {message.BalanceUpdatedSucceeded} ");
            await _transaction.UpdateStatus(message.BalanceUpdatedSucceeded, message.TransactionId);
            
        }
    }
}
