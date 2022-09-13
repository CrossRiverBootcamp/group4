
namespace Transaction.DAL.Interfaces
{
    public interface IUpdateTransactionStatusRepository
    {
        Task UpdateTransactionAsync(bool status, Guid transactionId);
        Task UpdateReasonFailedAsync(string reason, Guid transactionId);
    }
}
