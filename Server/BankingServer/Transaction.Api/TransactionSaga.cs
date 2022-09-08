using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Services.Interfaces;

namespace Transaction.Api
{
    public class TransactionSaga : Saga<TransactionData>,
        IAmStartedByMessages<TransactionPayloaded>,
        IHandleMessages<BalanceUpdated>
    {
        private readonly IUpdateTransactionStatusService _updateTransaction;
        public TransactionSaga(IUpdateTransactionStatusService updateTransaction)
        {
            _updateTransaction = updateTransaction;
        }
        static BalanceUpdated balanceUpdated = new();
        static ILog log = LogManager.GetLogger<TransactionSaga>();
        public async Task Handle(TransactionPayloaded message, IMessageHandlerContext context)
        {
            log.Info($"Received TransactionPayloaded, TransactionId = {message.TransactionId} ...");
           
            await context.Send(message);
            Data.GetEventPayload = true;
            //await ProccessTransaction(context);
        }
        public async Task/*<bool>*/ Handle(BalanceUpdated message, IMessageHandlerContext context)
        {
            try
            {
                await _updateTransaction.UpdateStatus(message.BalanceUpdatedSucceeded, message.TransactionId);
                Data.IsBalanceUpdated = true;
                log.Info($"Received BalanceUpdated true, TransactionId = {message.TransactionId} ...");

            }
            catch
            {
                Data.IsBalanceUpdated = false;
                log.Info($"Received BalanceUpdated false, TransactionId = {message.TransactionId} ...");

            }
            //bool flag = ProccessTransaction(context);
            MarkAsComplete();
            //return flag;    
        }
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.TransactionId)
                .ToMessage<TransactionPayloaded>(message => message.TransactionId);

            mapper.MapSaga(sagaData => sagaData.TransactionId)
                .ToMessage<BalanceUpdated>(message => message.TransactionId);
        }
        //private bool ProccessTransaction(IMessageHandlerContext context)
        //{
        //    if (Data.GetEventPayload && Data.IsBalanceUpdated)
        //    {
        //        //subscriber.Status = SubscriberStatus.Succeeded;
        //        //await context.Publish(subscriber);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


    }
}
