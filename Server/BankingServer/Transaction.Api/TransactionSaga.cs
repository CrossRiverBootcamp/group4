using AutoMapper;
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
        private readonly ILogger<TransactionSaga> _logger;

        public TransactionSaga(IUpdateTransactionStatusService updateTransaction, ILogger<TransactionSaga> logger)
        {
            _updateTransaction = updateTransaction;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TransactionMap>();
            });
            _mapper = config.CreateMapper();
            _logger = logger;
        }
        string messageLog;
        //static BalanceUpdated balanceUpdated = new();
        static ILog log = LogManager.GetLogger<TransactionSaga>();
        public async Task Handle(TransactionPayloaded message, IMessageHandlerContext context)
        {
            messageLog = $"Received TransactionPayloaded, TransactionId = {message.TransactionId} ...";
            log.Info(messageLog);
            TransactionPayload transaction = _mapper.Map<TransactionPayload>(message);
            await context.Send(transaction);
            Data.GetEventPayload = true;
        }
        public async Task Handle(BalanceUpdated message, IMessageHandlerContext context)
        {
            messageLog = $"In saga handler for balanceUpdated, TransactionId = {message.TransactionId} ...";
            log.Info(messageLog);
            try
            {
                await _updateTransaction.UpdateStatusAsync(message.BalanceUpdatedSucceeded, message.TransactionId);
                if (message.FailureReason != null)
                {
                    await _updateTransaction.UpdateReasonFailedAsync(message.FailureReason, message.TransactionId);
                    Data.IsBalanceUpdated = false;
                    messageLog = $"Couldn't execute transcation because {message.FailureReason} , TransactionId = {message.TransactionId} ...";
                    log.Error(messageLog);
                    _logger.LogError(messageLog);
                }
                else
                {
                    Data.IsBalanceUpdated = true;
                    messageLog = $"Balance was updated, transcation succeeded , TransactionId = {message.TransactionId} ...";
                    log.Info(messageLog);
                    _logger.LogInformation(messageLog);
                }
            }
            catch(Exception ex)
            {
                Data.IsBalanceUpdated = false;
                messageLog = $"Couldn't update transaction balance, TransactionId = {message.TransactionId} ...";
                log.Error(messageLog);
                _logger.LogError(messageLog);
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
