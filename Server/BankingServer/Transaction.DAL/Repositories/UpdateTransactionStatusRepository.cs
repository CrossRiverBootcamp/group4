using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task UpdateTransaction(bool status, Guid transactionId)
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
