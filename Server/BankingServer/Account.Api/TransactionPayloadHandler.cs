using Account.Services.Interfaces;
using Messages;
using NServiceBus;
using NServiceBus.Logging;


namespace Account.Api
{
    public class TransactionPayloadHandler : IHandleMessages<TransactionPayload>
    {
        private readonly IAccountSagaService _accountSagaService;
        private readonly IOperationService _operationService;
        static ILog log = LogManager.GetLogger<TransactionPayloadHandler>();
        static BalanceUpdated balanceUpdated = new();
        public TransactionPayloadHandler(IAccountSagaService accountSagaService, IOperationService operationService)
        {
            _accountSagaService = accountSagaService;
            _operationService = operationService;
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
                            log.Info($"Balance has been updated. TransactionId= {message.TransactionId}");
                            try
                            {
                                await _operationService.AddToHistoryTableAsync(message);
                                log.Info($"Manage to add to history table, TransactionId = {message.TransactionId} ...");
                            }
                            catch
                            {
                                log.Info($"Failed to add to history table, TransactionId = {message.TransactionId} ...");
                            }
                            log.Info($"Received TransactionPayload command updated, TransactionId = {message.TransactionId} ...");
                        }
                        catch
                        {
                            balanceUpdated.BalanceUpdatedSucceeded = false;
                            log.Info($"Received TransactionPayload command didn't update, TransactionId = {message.TransactionId} ...");
                        }
                    }
                    else
                    {
                        balanceUpdated.FailureReason = "Transaction failed because there is not enough money in your account";
                        balanceUpdated.BalanceUpdatedSucceeded = false;
                        log.Info($"Received TransactionPayload command didn't update, TransactionId = {message.TransactionId} ...");
                    }
                }
                else
                {
                    balanceUpdated.FailureReason = "The account you are trying to transfer to doesn't exist";
                    balanceUpdated.BalanceUpdatedSucceeded = false;
                    log.Info($"Received TransactionPayload command didn't update, TransactionId = {message.TransactionId} ...");
                }
            }
            else
            {
                balanceUpdated.BalanceUpdatedSucceeded = false;
                balanceUpdated.FailureReason = "The account you are trying to transfer from doesn't exist";
                log.Info($"Received TransactionPayload command didn't update, TransactionId = {message.TransactionId} ...");
            }
            await context.Publish(balanceUpdated);
        }
    }
}
