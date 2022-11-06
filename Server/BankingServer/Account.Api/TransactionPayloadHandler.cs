using Account.Services.Interfaces;
using Messages;
using NServiceBus;
using NServiceBus.Logging;


namespace Account.Api
{
    public class TransactionPayloadHandler : IHandleMessages<TransactionPayload>
    {
        private readonly ILogger<TransactionPayloadHandler> _logger;
        private readonly IAccountSagaService _accountSagaService;
        private readonly IOperationService _operationService;
        static ILog log = LogManager.GetLogger<TransactionPayloadHandler>();
        static BalanceUpdated balanceUpdated = new();
        string messageLog;
        public TransactionPayloadHandler(IAccountSagaService accountSagaService, IOperationService operationService, ILogger<TransactionPayloadHandler> logger)
        {
            _accountSagaService = accountSagaService;
            _operationService = operationService;
            _logger = logger;
        }

        //Handler for Transaction payload message
        //The handler checks if the transaction is valid and if it is updates balance for both accounts
        //sends message back to saga- balance updated true or false
        public async Task Handle(TransactionPayload message, IMessageHandlerContext context)
        {
            balanceUpdated.TransactionId = message.TransactionId;
            log.Info($"In Account handler, TransactionId = {message.TransactionId} ...");
            if (await _accountSagaService.CheckIdValidAsync(message.FromAccountId))
            {
                if (await _accountSagaService.CheckIdValidAsync(message.ToAccountId))
                {
                    if (await _accountSagaService.CheckBalanceAsync(message.FromAccountId, message.Amount))
                    {
                        try
                        {
                            await _accountSagaService.UpdateBalanceAsync(message.FromAccountId, message.ToAccountId, message.Amount);
                            balanceUpdated.BalanceUpdatedSucceeded = true;
                            messageLog = $"Balance has been updated. TransactionId= {message.TransactionId}";
                            log.Info(messageLog);
                            _logger.LogInformation(messageLog);
                            try
                            {
                                await _operationService.AddToHistoryTableAsync(message);
                                messageLog = $"Manage to add to history table, TransactionId = {message.TransactionId} ...";
                                log.Info(messageLog);
                                _logger.LogInformation(messageLog);
                            }
                            catch
                            {
                                messageLog = $"Failed to add to history table, TransactionId = {message.TransactionId} ...";
                                log.Error(messageLog);
                                _logger.LogError(messageLog);
                            }
                            messageLog = $"Received TransactionPayload command updated, TransactionId = {message.TransactionId} ...";
                            log.Info(messageLog);
                            _logger.LogInformation(messageLog);
                        }
                        catch
                        {
                            balanceUpdated.BalanceUpdatedSucceeded = false;
                            messageLog = $"Received TransactionPayload command didn't update, TransactionId = {message.TransactionId} ...";
                            log.Error(messageLog);
                            _logger.LogError(messageLog);
                        }
                    }
                    else
                    {
                        balanceUpdated.FailureReason = "Transaction failed because there is not enough money in your account";
                        balanceUpdated.BalanceUpdatedSucceeded = false;
                        messageLog = $"Received TransactionPayload command didn't update, TransactionId = {message.TransactionId} ...";
                        log.Error(messageLog);
                        _logger.LogError(messageLog);
                    }
                }
                else
                {
                    balanceUpdated.FailureReason = "The account you are trying to transfer to doesn't exist";
                    balanceUpdated.BalanceUpdatedSucceeded = false;
                    messageLog = $"Received TransactionPayload command didn't update, TransactionId = {message.TransactionId} ...";
                    log.Error(messageLog);
                    _logger.LogError(messageLog);
                }
            }
            else
            {
                balanceUpdated.FailureReason = "The account you are trying to transfer from doesn't exist";
                balanceUpdated.BalanceUpdatedSucceeded = false;
                messageLog = $"Received TransactionPayload command didn't update, TransactionId = {message.TransactionId} ...";
                log.Error(messageLog);
                _logger.LogError(messageLog);
            }
            await context.Publish(balanceUpdated);
        }
    }
}
