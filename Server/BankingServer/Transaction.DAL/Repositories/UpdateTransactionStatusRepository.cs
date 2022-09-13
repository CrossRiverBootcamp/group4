using Microsoft.EntityFrameworkCore;
using Transaction.DAL.Entities;
using Transaction.DAL.Interfaces;

namespace Transaction.DAL.Repositories
{
    public class UpdateTransactionStatusRepository : IUpdateTransactionStatusRepository
    {
        private readonly IDbContextFactory<TransactionDBContext> _factory;
        public UpdateTransactionStatusRepository(IDbContextFactory<TransactionDBContext> factory)
        {
            _factory = factory;
        }

        public async Task UpdateReasonFailedAsync(string reason, Guid transactionId)
        {
            using var context = _factory.CreateDbContext();
            TransactionEntity transactionEntity = await context.Transactions.FirstOrDefaultAsync(t => t.Id.Equals(transactionId));
            transactionEntity.FailureReason = reason;
            context.SaveChangesAsync();

        }

        public async Task UpdateTransactionAsync(bool status, Guid transactionId)
        {
            using var context = _factory.CreateDbContext();
            TransactionEntity transactionEntity = await context.Transactions.FirstOrDefaultAsync(t => t.Id.Equals(transactionId));
            if (transactionEntity == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                if (status)
                {
                    transactionEntity.Status = TransactionStatus.Succeeded;
                }
                else
                {
                    transactionEntity.Status = TransactionStatus.Failed;
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
