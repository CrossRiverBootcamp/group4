using Transaction.DAL.Interfaces;
using Transaction.Services.Interfaces;

namespace Transaction.Services.Services
{
    public class UpdateTransactionStatusService: IUpdateTransactionStatusService
    {
        private readonly IUpdateTransactionStatusRepository _transaction;
        public UpdateTransactionStatusService(IUpdateTransactionStatusRepository transaction)
        {
            _transaction = transaction;
        }

        public async Task UpdateReasonFailedAsync(string reason, Guid transactionId)
        {
            await _transaction.UpdateReasonFailedAsync(reason, transactionId);
        }

        public async Task UpdateStatusAsync(bool status, Guid transactionId)
        {
            await _transaction.UpdateTransactionAsync(status, transactionId);
        }
    }
}
