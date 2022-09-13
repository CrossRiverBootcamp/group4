using Microsoft.EntityFrameworkCore;
using Transaction.DAL.Entities;
using Transaction.DAL.Interfaces;

namespace Transaction.DAL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDbContextFactory<TransactionDBContext> _factory;
        public TransactionRepository(IDbContextFactory<TransactionDBContext> factory)
        {
            _factory = factory;
        }
        public async Task AddTransactionAsync(TransactionEntity transaction) {
            using var context = _factory.CreateDbContext();
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();
        }
    }
}
