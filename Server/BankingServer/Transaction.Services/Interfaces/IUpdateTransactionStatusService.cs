

namespace Transaction.Services.Interfaces
{
    public interface IUpdateTransactionStatusService
    {
        Task UpdateStatusAsync(bool status, Guid transactionId);
        Task UpdateReasonFailedAsync(string reason, Guid transactionId); 
    }
}
