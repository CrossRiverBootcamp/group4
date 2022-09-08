using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Services.Interfaces;

namespace Transaction.NSB
{
    public class TransactionSaga : Saga<TransactionData>,
        IAmStartedByMessages<TransactionPayload>,
        IHandleMessages<BalanceUpdated>
    {
        private readonly IUpdateTransactionStatusService _updateTransaction;
        public TransactionSaga(IUpdateTransactionStatusService updateTransaction)
        {
            _updateTransaction = updateTransaction;
        }
        static BalanceUpdated balanceUpdated = new();
        static ILog log = LogManager.GetLogger<TransactionSaga>();
        public async Task Handle(TransactionPayload message, IMessageHandlerContext context)
        {
            //log.Info($"Received TransactionPayloaded, TransactionId = {message.TransactionId} ...");
           
            //balanceUpdated.TransactionId = message.TransactionId;
            //Data.IsTransactionFinished = true;
            await context.Send(message);
            await ProccessTransaction(context);
        }
        //public async Task Handle(TransactionStatusUpdateCompleted message, IMessageHandlerContext context)
        //{
        //    log.Info($"Received TransactionStatusUpdateCompleted, TransactionId = {message.TransactionId} ...");
        //    Data.IsTransactionStatusUpdateCompleted = true;
        //    await ProccessTransaction(context);
        //}
        public async Task Handle(BalanceUpdated message, IMessageHandlerContext context)
        {
            await _updateTransaction.UpdateStatus(message.BalanceUpdatedSucceeded, message.TransactionId);
        }
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.TransactionId)
                .ToMessage<TransactionPayload>(message => message.TransactionId);

            mapper.MapSaga(sagaData => sagaData.TransactionId)
                .ToMessage<BalanceUpdated>(message => message.TransactionId);
        }
        private async Task ProccessTransaction(IMessageHandlerContext context)
        {
            if (Data.IsTransactionFinished && Data.IsTransactionStatusUpdateCompleted)
            {
                //subscriber.Status = SubscriberStatus.Succeeded;
                //await context.Publish(subscriber);
                MarkAsComplete();
            }
        }


    }
}
