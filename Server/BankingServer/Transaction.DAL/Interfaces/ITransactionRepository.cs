using Transaction.DAL.Entities;

namespace Transaction.DAL.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddTransactionAsync(TransactionEntity transaction);
    }
}
