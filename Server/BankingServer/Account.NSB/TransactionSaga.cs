using Account.DAL.Interfaces;
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
    public class TransactionSaga : Saga<TransactionData>,
        IAmStartedByMessages<TransactionPayloaded>,
        IHandleMessages<TransactionStatusUpdateCompleted>
    {
        private readonly IAccountSagaRepository _accountSagaRepository;
        public TransactionSaga(IAccountSagaRepository accountSagaRepository)
        {
            _accountSagaRepository = accountSagaRepository;
        }
        static BalanceUpdated balanceUpdated = new();
        static ILog log = LogManager.GetLogger<TransactionSaga>();
        public async Task Handle(TransactionPayloaded message, IMessageHandlerContext context)
        {
            log.Info($"Received TransactionPayloaded, TransactionId = {message.TransactionId} ...");
            if (await _accountSagaRepository.CheckIdValid(message.FromAccount)&& await _accountSagaRepository.CheckIdValid(message.ToAccount)
                &&await _accountSagaRepository.CheckBalance(message.FromAccount, message.Amount))
            {
                try
                {
                    await _accountSagaRepository.UpdateBalance(message.FromAccount, message.ToAccount, message.Amount);
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
            balanceUpdated.TransactionId = message.TransactionId;
            Data.IsTransactionFinished = true;
            await context.Publish(balanceUpdated);
            await ProccessTransaction(context);
        }
        public async Task Handle(TransactionStatusUpdateCompleted message, IMessageHandlerContext context)
        {
            log.Info($"Received TransactionStatusUpdateCompleted, TransactionId = {message.TransactionId} ...");
            Data.IsTransactionStatusUpdateCompleted = true;
            await ProccessTransaction(context);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.TransactionId)
                .ToMessage<TransactionPayloaded>(message => message.TransactionId);

            mapper.MapSaga(sagaData => sagaData.TransactionId)
                .ToMessage<TransactionStatusUpdateCompleted>(message => message.TransactionId);
        }
        private async Task ProccessTransaction(IMessageHandlerContext context)
        {
            if (Data.IsTransactionFinished && Data.IsTransactionStatusUpdateCompleted)
            {
                subscriber.Status = SubscriberStatus.Succeeded;
                await context.Publish(subscriber);
                MarkAsComplete();
            }
        }

    }
}
