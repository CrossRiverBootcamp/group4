﻿using AutoMapper;
using Messages;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Services.Interfaces;
using Transaction.Services.Mapping;

namespace Transaction.Api
{
    public class TransactionSaga : Saga<TransactionData>,
        IAmStartedByMessages<TransactionPayloaded>,
        IHandleMessages<BalanceUpdated>
    {
        private readonly IUpdateTransactionStatusService _updateTransaction;
        private readonly IMapper _mapper;

        public TransactionSaga(IUpdateTransactionStatusService updateTransaction)
        {
            _updateTransaction = updateTransaction;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TransactionMap>();
            });
            _mapper = config.CreateMapper();
        }
        //static BalanceUpdated balanceUpdated = new();
        static ILog log = LogManager.GetLogger<TransactionSaga>();
        public async Task Handle(TransactionPayloaded message, IMessageHandlerContext context)
        {
            log.Info($"Received TransactionPayloaded, TransactionId = {message.TransactionId} ...");
            TransactionPayload transaction = _mapper.Map<TransactionPayload>(message);
            await context.Send(transaction);
            Data.GetEventPayload = true;
        }
        public async Task Handle(BalanceUpdated message, IMessageHandlerContext context)
        {
            log.Info($"In saga handler for balanceUpdated, TransactionId = {message.TransactionId} ...");
            try
            {
                await _updateTransaction.UpdateStatusAsync(message.BalanceUpdatedSucceeded, message.TransactionId);
                if (message.FailureReason != null)
                {
                    await _updateTransaction.UpdateReasonFailedAsync(message.FailureReason, message.TransactionId);
                    Data.IsBalanceUpdated = false;
                    log.Info($"Couldn't execute transcation because {message.FailureReason} , TransactionId = {message.TransactionId} ...");
                }
                else
                {
                    Data.IsBalanceUpdated = true;
                    log.Info($"Balance was updated, transcation succeeded , TransactionId = {message.TransactionId} ...");
                }
            }
            catch(Exception ex)
            {
                Data.IsBalanceUpdated = false;
                log.Info($"Couldn't update transaction balance, TransactionId = {message.TransactionId} ...");

            }
            MarkAsComplete();
        }
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.TransactionId)
                .ToMessage<TransactionPayloaded>(message => message.TransactionId)
                .ToMessage<BalanceUpdated>(message => message.TransactionId);
        }
    }
}
