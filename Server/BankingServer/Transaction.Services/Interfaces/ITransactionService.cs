using NServiceBus;
using Transaction.DTO;

namespace Transaction.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> SendTransactionAsync(TransactionDto transactionDto, IMessageSession messageSession);
    }
}
